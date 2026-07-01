using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Models;
using Dan.Plugin.Skatteetaten.Models.Arbeidsgiveravgift;
using Dan.Plugin.Skatteetaten.Models.Dtos;
using Dan.Plugin.Skatteetaten.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DanConstants = Dan.Common.Constants;

namespace Dan.Plugin.Skatteetaten
{
    public class PayrollTax
    {
        private HttpClient _client;
        private ApplicationSettings _settings;
        private ILogger _logger;
        private IEvidenceSourceMetadata _metadata;

        public PayrollTax(IHttpClientFactory factory, IOptions<ApplicationSettings> settings, ILoggerFactory loggerFactory, IEvidenceSourceMetadata evidenceSourceMetadata)
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
            return await EvidenceSourceResponse.CreateResponse(req, () => GetPayrollTaxFromSkeAsync(evidenceHarvesterRequest));
        }

        private async Task<List<EvidenceValue>> GetPayrollTaxFromSkeAsync(EvidenceHarvesterRequest evidenceHarvesterRequest)
        {
            var url = $"{_settings.ArbeidsgiveravgiftEndpoint}/v1/ebevis/{evidenceHarvesterRequest.OrganizationNumber}";
            var result = await Helpers.HarvestFromSke<PayrollTaxModel>(evidenceHarvesterRequest, _logger, _client, HttpMethod.Get, url, _settings);

            var dto = new ArbeidsgiveravgiftDto(result);

            var ecb = new EvidenceBuilder(_metadata, "Arbeidsgiveravgift");
            ecb.AddEvidenceValue("default", JsonConvert.SerializeObject(dto), Constants.Source, false);
            return ecb.GetEvidenceValues();
        }
    }
}
