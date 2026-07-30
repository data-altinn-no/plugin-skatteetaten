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
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DanConstants = Dan.Common.Constants;

namespace Dan.Plugin.Skatteetaten
{
    public class VATReport
    {
        private HttpClient _client;
        private ApplicationSettings _settings;
        private ILogger _logger;
        private IEvidenceSourceMetadata _metadata;

        public VATReport(IHttpClientFactory factory, IOptions<ApplicationSettings> settings, ILoggerFactory loggerFactory, IEvidenceSourceMetadata evidenceSourceMetadata)
        {
            _client = factory.CreateClient(DanConstants.SafeHttpClient);
            _settings = settings.Value;
            _metadata = evidenceSourceMetadata;
            _logger = loggerFactory.CreateLogger<VATReport>();
        }

        [Function("MvaMeldingsOpplysning")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            return await EvidenceSourceResponse.CreateResponse(req, ()=> GetFromSKE(evidenceHarvesterRequest));
        }

        private async Task<List<EvidenceValue>> GetFromSKE(EvidenceHarvesterRequest evidenceHarvesterRequest)
        {
            var url = $"{_settings.MvaMeldingsOpplysningEndpoint}/v1/ebevis/{evidenceHarvesterRequest.OrganizationNumber}";
            var result = await Helpers.HarvestFromSke<VATReportModel>(evidenceHarvesterRequest, _logger, _client, HttpMethod.Get, url);

            DateTime delivered = result.levert;
            string orgNo = result.forespurteOrganisasjon;
            string VATregularBusiness = JsonConvert.SerializeObject(result.mvaAlminneligNaering);
            // string responsibleOrgForVatReport = JsonConvert.SerializeObject(result.ansvarligForMvaMelding);

            var ecb = new EvidenceBuilder(_metadata, "MvaMeldingsOpplysning");

            ecb.AddEvidenceValue($"levert", delivered);
            ecb.AddEvidenceValue($"forespurteOrganisasjon", orgNo);
            ecb.AddEvidenceValue($"mvaAlminneligNaering", VATregularBusiness);
            // ecb.AddEvidenceValue($"ansvarligForMvaMelding", responsibleOrgForVatReport);

            return ecb.GetEvidenceValues();
        }
    }
}
