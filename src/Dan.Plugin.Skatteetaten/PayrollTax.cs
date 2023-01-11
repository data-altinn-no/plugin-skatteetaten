using Dan.Common.Exceptions;
using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Models.Arbeidsgiveravgift;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Options;
using Dan.Plugin.Skatteetaten.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Dan.Plugin.Skatteetaten.Utilities;
using Microsoft.Azure.Functions.Worker.Http;
using DanConstants = Dan.Common.Constants;
namespace Dan.Plugin.Skatteetaten
{

    public class PayrollTax
    {
        private HttpClient _client;
        private ApplicationSettings _settings;
        private ILogger _logger;
        private IEvidenceSourceMetadata _metadata;

        public PayrollTax(IHttpClientFactory factory, IOptions<ApplicationSettings> settings, LoggerFactory loggerFactory, IEvidenceSourceMetadata evidenceSourceMetadata)
        {
            _client = factory.CreateClient(DanConstants.SafeHttpClient);
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<PayrollTax>();
            _metadata = evidenceSourceMetadata;
        }

        [Function("Arbeidsgiveravgift")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")]
            HttpRequestData req,
            FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            return await EvidenceSourceResponse.CreateResponse(req, ()=> GetPayrollTaxFromSkeAsync(evidenceHarvesterRequest, _logger));          
        }

        private async Task<List<EvidenceValue>> GetPayrollTaxFromSkeAsync(EvidenceHarvesterRequest evidenceHarvesterRequest, ILogger logger)
        {
            var url = $"{_settings.ServiceEndpoint}/api/arbeidsgiveravgift/v1/ebevis/{evidenceHarvesterRequest.OrganizationNumber}";
            var result = await Helpers.HarvestFromSke(evidenceHarvesterRequest, logger, _client, HttpMethod.Get, url);

            var ecb = new EvidenceBuilder(_metadata, "Arbeidsgiveravgift");
            try
            {
                ecb.AddEvidenceValue($"levert", result.levert);
            }
            catch(Exception ex)
            {
                logger.LogError("Error parsing 'levert' : " + ex.ToString());
            }
            
            try
            {
                ecb.AddEvidenceValue($"forespurteOrganisasjon", result.forespurteOrganisasjon);
            }
            catch(Exception ex)
            {
                logger.LogError("Error parsing 'forespurteOrganisasjon' : " + ex.ToString());
            }

            try
            {
                ecb.AddEvidenceValue($"arbeidsgiveravgifter", JsonConvert.SerializeObject(result.arbeidsgiveravgifter), Constants.Source, false);
            }
            catch(Exception ex)
            {
                logger.LogError("Error parsing 'arbeidsgiveravgifter' : " + ex.ToString());
            }

            return ecb.GetEvidenceValues();
        }
    }
}
