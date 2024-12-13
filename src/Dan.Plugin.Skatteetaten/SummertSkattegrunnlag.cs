using Dan.Common.Exceptions;
using Dan.Common.Extensions;
using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Models;
using Dan.Plugin.Skatteetaten.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dan.Plugin.Skatteetaten.Models.Dtos;
using DanConstants = Dan.Common.Constants;

namespace Dan.Plugin.Skatteetaten
{
    public class SummertSkattegrunnlag
    {
        private List<KeyValuePair<string, string>> serviceContextRightsPkg = new List<KeyValuePair<string, string>>();

        private HttpClient _client;
        private ApplicationSettings _settings;
        private ILogger _logger;
        private IEvidenceSourceMetadata _metadata;

        public SummertSkattegrunnlag(IHttpClientFactory factory, IOptions<ApplicationSettings> settings, IEvidenceSourceMetadata evidenceSourceMetadata, ILoggerFactory loggerFactory)
        {
            _client = factory.CreateClient(DanConstants.SafeHttpClient);
            _settings = settings.Value;
            _metadata = evidenceSourceMetadata;
            _logger = loggerFactory.CreateLogger<SummertSkattegrunnlag>();

            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("DigitaleHelgeland", "kommuneforeldrebetaling"));
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("OED", "husbankenBostoette"));
        }

        [Function("SummertSkattegrunnlagOED")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            var rightspackage = serviceContextRightsPkg.Where(v => v.Key == evidenceHarvesterRequest.ServiceContext).FirstOrDefault();


            if (string.IsNullOrEmpty(rightspackage.Value))
            {
                _logger.LogError($"SummertSkattegrunnlag: rettighetspakke not defined for {evidenceHarvesterRequest.ServiceContext}");
                throw new EvidenceSourcePermanentServerException(Constants.ERROR_CCR_UPSTREAM_ERROR, "No rights package available for servicecontext");
            }

            return await EvidenceSourceResponse.CreateResponse(req, () => GetSkattegrunnlagOED(evidenceHarvesterRequest, rightspackage.Value));
        }

        [Function("SummertSkattegrunnlag")]
        public async Task<HttpResponseData> Skattegrunnlag([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            var rightspackage = serviceContextRightsPkg.Where(v => v.Key == evidenceHarvesterRequest.ServiceContext).FirstOrDefault();

            if (string.IsNullOrEmpty(rightspackage.Value))
            {
                _logger.LogError($"SummertSkattegrunnlag: rettighetspakke not defined for {evidenceHarvesterRequest.ServiceContext}");
                throw new EvidenceSourcePermanentServerException(Constants.ERROR_CCR_UPSTREAM_ERROR, "No rights package available for servicecontext");
            }
            return await EvidenceSourceResponse.CreateResponse(req, () => GetSkattegrunnlag(evidenceHarvesterRequest, rightspackage.Value));
        }

        private async Task<List<EvidenceValue>> GetSkattegrunnlagOED(EvidenceHarvesterRequest req, string rightsPackage)
        {
            var taxData = await GetSkattegrunnlagFromSKE(req, rightsPackage, "oppgjoer");

            var bruttoformue = taxData.Grunnlag.Where(x => x.TekniskNavn == "bruttoformue").FirstOrDefault();
            var gjeld = taxData.Grunnlag.Where(x => x.TekniskNavn == "samletGjeld").FirstOrDefault();

            var itemData = new SkattItemResponse()
            {
                Utkast = false,
                Bruttoformue = bruttoformue != null ? bruttoformue.Beloep : 0,
                SamletGjeld = gjeld != null ? gjeld.Beloep : 0,
                Aar = int.Parse(taxData.InntektsAar)
            };

            var ecb = new EvidenceBuilder(_metadata, "SummertSkattegrunnlagOED");
            ecb.AddEvidenceValue($"default", JsonConvert.SerializeObject(itemData), "Skatteetaten", false);
            return ecb.GetEvidenceValues();
        }

        private async Task<List<EvidenceValue>> GetSkattegrunnlag(EvidenceHarvesterRequest req, string rightsPackage)
        {
            string stadieParam = string.Empty;
            try
            {
                req.TryGetParameter("stadie", out stadieParam);
            } catch (Exception) { }

            if (string.IsNullOrEmpty(stadieParam.Trim()))
                stadieParam = "oppgjoer";

            var taxData = await GetSkattegrunnlagFromSKE(req, rightsPackage, stadieParam);

            var ecb = new EvidenceBuilder(_metadata, "SummertSkattegrunnlag");
            ecb.AddEvidenceValue($"default", JsonConvert.SerializeObject(taxData), "Skatteetaten", false);
            return ecb.GetEvidenceValues();
        }

        private async Task<SummertSkattegrunnlagDto> GetSkattegrunnlagFromSKE(EvidenceHarvesterRequest evidenceHarvesterRequest, string rightsPackage, string stadie)
        {
            var urlRecent = $"{_settings.SisteTilgjengeligeSkatteoppgjoerEndpoint}/v1/{evidenceHarvesterRequest.OrganizationNumber}";
            var mostRecentData = await Helpers.HarvestFromSke<TilgjengeligData>(evidenceHarvesterRequest, _logger, _client, HttpMethod.Get, urlRecent, _settings);

            var url = $"{_settings.SummertSkattegrunnlagEndpoint}/v2/{stadie}/{rightsPackage}/{mostRecentData.sisteTilgjengeligePeriode}/{evidenceHarvesterRequest.OrganizationNumber}";
            var skattegrunnlag = await Helpers.HarvestFromSke<SummertSkattegrunnlagModel>(evidenceHarvesterRequest, _logger, _client, HttpMethod.Get, url, _settings);
            return new SummertSkattegrunnlagDto(skattegrunnlag);
        }
    }
}
