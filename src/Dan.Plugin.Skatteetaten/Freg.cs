using System;
using Dan.Common.Exceptions;
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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dan.Common.Extensions;
using DanConstants = Dan.Common.Constants;

namespace Dan.Plugin.Skatteetaten
{
    public class Freg
    {
        private HttpClient _client;
        private ApplicationSettings _settings;

        private ILogger _logger;

        private IEvidenceSourceMetadata _metadata;

        private const string PartPersonRelation = "relasjon-utvidet";
        private const string PartPersonBasis = "person-basis";

        private const string ENV = "$$ENV$$";
        private const string PART = "$$PART$$";
        private const string PARTS = "$$PARTS$$";
        private const string PERSON = "$$PERSON$$";


        private List<KeyValuePair<string, string>> serviceContextRightsPkg = new List<KeyValuePair<string, string>>();

        public Freg(IHttpClientFactory factory, IOptions<ApplicationSettings> settings, ILoggerFactory loggerFactory, IEvidenceSourceMetadata evidenceSourceMetadata)
        {
            _client = factory.CreateClient(DanConstants.SafeHttpClient);
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<Freg>();
            _metadata = evidenceSourceMetadata;

            //servicecontextname, url
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("DigitaleHelgeland", $"{ENV}folkeregisteret/offentlig-med-hjemmel/api/v1/personer/{PERSON}?part={PART}"));
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("Reelle rettighetshavere", $"{ENV}folkeregisteret/api/offentligutenhjemmel/v1/personer/{PERSON}?part={PART}"));
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("DigitalGravferdsmelding", $"{ENV}folkeregisteret/offentlig-med-hjemmel/api/v1/personer/{PERSON}?part={PART}"));
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("OED", $"{ENV}folkeregisteret/offentlig-med-hjemmel/api/v1/personer/{PERSON}"));
        }

        private string GetUrlForServiceContext(string ssn, string serviceContext, string part = "", string parts = "")
        {
           var kvp = serviceContextRightsPkg.Where(x => x.Key == serviceContext).First();

           if (string.IsNullOrEmpty(kvp.Value))
           {
               _logger.LogError($"SummertSkattegrunnlag: rettighetspakke not defined for {serviceContext}");
               throw new EvidenceSourcePermanentServerException(Constants.ERROR_CCR_UPSTREAM_ERROR, "No rights package available for servicecontext");
           }

           var url = kvp.Value.Replace(ENV, _settings.FregEnvironment);
           url = url.Replace(PERSON, ssn);
           url = url.Replace(PART, part);

            if (parts != String.Empty)
            {
                var tmp = parts.Split(",").ToList();

                foreach (string partItem in tmp)
                {
                    //Part is always set to some value - so we can just append further parts
                    url += $"&part={partItem.Trim()}";
                }
            }

            return url;
        }

        [Function("FregPersonRelasjonUtvidet")]
        public async Task<HttpResponseData> FregPersonRelasjonUtvidet([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            var url = GetUrlForServiceContext(evidenceHarvesterRequest.SubjectParty.GetAsString(false), evidenceHarvesterRequest.ServiceContext, evidenceHarvesterRequest.TryGetParameter("part", out string partParam) ? partParam : PartPersonRelation);
                
            return await EvidenceSourceResponse.CreateResponse(req, () => GetFregPersonRelasjonUtvidet(evidenceHarvesterRequest, url));
        }

        [Function("FregPerson")]
        public async Task<HttpResponseData> FregPerson([HttpTrigger(AuthorizationLevel.Function,"post", Route = null)] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            var url = GetUrlForServiceContext(evidenceHarvesterRequest.SubjectParty.GetAsString(false), evidenceHarvesterRequest.ServiceContext, evidenceHarvesterRequest.TryGetParameter("part", out string partParam) ? partParam : PartPersonBasis, evidenceHarvesterRequest.TryGetParameter("parts", out string partsParam) ? partsParam : string.Empty);

            return await EvidenceSourceResponse.CreateResponse(req, () => GetFregPerson(evidenceHarvesterRequest, url));
        }

        private async Task<List<EvidenceValue>> GetFregPerson(EvidenceHarvesterRequest req, string url)
        {
            //req.MPToken = req.MPToken ?? GetToken(req.ServiceContext);

            var result = await Helpers.HarvestFromSke(req, _logger, _client, HttpMethod.Get, url);

            var ecb = new EvidenceBuilder(_metadata, "FregPerson");
            ecb.AddEvidenceValue("default", JsonConvert.SerializeObject(result), "Skatteetaten", false);

            return ecb.GetEvidenceValues();
        }

        private async Task<List<EvidenceValue>> GetFregPersonRelasjonUtvidet(EvidenceHarvesterRequest req, string url)
        {
            //req.MPToken = req.MPToken ?? GetToken(req.ServiceContext);

            var result = await Helpers.HarvestFromSke(req, _logger, _client, HttpMethod.Get, url);

            var ecb = new EvidenceBuilder(_metadata, "FregPersonRelasjonUtvidet");
            ecb.AddEvidenceValue("default", JsonConvert.SerializeObject(result), "Skatteetaten", false);

            return ecb.GetEvidenceValues();
        }
    }
}
