using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DanConstants = Dan.Common.Constants;

namespace Dan.Plugin.Skatteetaten
{
    /// <summary>
    /// This class implements the Azure Function entry points for all the functions implemented by this evidence source. 
    /// </summary>   
    public class ArrearsV2
    {
        private HttpClient _client;
        private ApplicationSettings _settings;
        private readonly ILogger _logger;
        private IEvidenceSourceMetadata _evidenceSourceMetadata;

        public ArrearsV2(IHttpClientFactory factory, IOptions<ApplicationSettings> settings, ILoggerFactory loggerFactory, IEvidenceSourceMetadata metadata)
        {
            _client = factory.CreateClient(DanConstants.SafeHttpClient);
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<ArrearsV2>();
            _evidenceSourceMetadata = metadata;
        }

        [Function("RestanserV2")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();
            return await EvidenceSourceResponse.CreateResponse(req, ()=> GetArrearsFromSkeAsync(evidenceHarvesterRequest));
        }

        private async Task<List<EvidenceValue>> GetArrearsFromSkeAsync(EvidenceHarvesterRequest evidenceHarvesterRequest)
        {
            var url = $"{_settings.ServiceEndpoint}/api/innkreving/restanser/v2/ebevis/{evidenceHarvesterRequest.OrganizationNumber}";
            dynamic result = await Helpers.HarvestFromSke(evidenceHarvesterRequest, _logger, _client, HttpMethod.Get, url);

            string orgNo = "";
            DateTime? delivered = null;
            string AGA = "";
            string withholdingDueAndUnpaid = "";
            string preemptiveTaxUnpaid = "";
            string remainingTaxes = "";
            string remainingAdditionalCharges = "";
            string VAT = "";

            if (result["levert"] != null)
            {
                delivered = result["levert"];
            }            

            if (result["forespurteOrganisasjon"] != null)
            {
                orgNo = result["forespurteOrganisasjon"];
            }

            if (result["restanser"]["arbeidsgiveravgift"]["forfaltOgUbetalt"] != null)
            {
                AGA = result["restanser"]["arbeidsgiveravgift"]["forfaltOgUbetalt"] + " NOK";
            }

            if (result["restanser"]["forskuddstrekk"]["forfaltOgUbetalt"] != null)
            {
                withholdingDueAndUnpaid = result["restanser"]["forskuddstrekk"]["forfaltOgUbetalt"] + " NOK";
            }  
            
            if (result["restanser"]["forskuddsskatt"]["forfaltOgUbetalt"] != null)
            {
                preemptiveTaxUnpaid = result["restanser"]["forskuddsskatt"]["forfaltOgUbetalt"] + " NOK";
            }

            if (result["restanser"]["restskatt"]["forfaltOgUbetalt"] != null)
            {
                remainingTaxes = result["restanser"]["restskatt"]["forfaltOgUbetalt"] + " NOK";
            }

            if (result["restanser"]["gebyr"]["forfaltOgUbetalt"] != null)
            {
                remainingAdditionalCharges = result["restanser"]["gebyr"]["forfaltOgUbetalt"] + " NOK";
            }

            if (result["restanser"]["merverdiavgift"]["forfaltOgUbetalt"] != null)
            {
                VAT = result["restanser"]["merverdiavgift"]["forfaltOgUbetalt"] + " NOK";
            }

            var ecb = new EvidenceBuilder(_evidenceSourceMetadata, "RestanserV2");

            ecb.AddEvidenceValue($"levert", delivered);
            ecb.AddEvidenceValue($"forespurteOrganisasjon", orgNo);
            ecb.AddEvidenceValue($"arbeidsgiveravgiftForfaltOgUbetalt", AGA);
            ecb.AddEvidenceValue($"forskuddstrekkForfaltOgUbetalt", withholdingDueAndUnpaid);
            ecb.AddEvidenceValue($"forskuddsskattForfaltOgUbetalt", preemptiveTaxUnpaid);
            ecb.AddEvidenceValue($"restskattForfaltOgUbetalt", remainingTaxes);
            ecb.AddEvidenceValue($"gebyrForfaltOgUbetalt", remainingAdditionalCharges);
            ecb.AddEvidenceValue($"merverdiavgiftForfaltOgUbetalt", VAT);
            return ecb.GetEvidenceValues();
        }
    }
}
