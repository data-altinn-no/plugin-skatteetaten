using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Models;
using Dan.Plugin.Skatteetaten.Models.Arrears;
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
            return await EvidenceSourceResponse.CreateResponse(req, () => GetArrearsFromSkeAsync(evidenceHarvesterRequest));
        }

        private async Task<List<EvidenceValue>> GetArrearsFromSkeAsync(EvidenceHarvesterRequest evidenceHarvesterRequest)
        {
            var url = $"{_settings.RestanserEndpoint}/v2/ebevis/{evidenceHarvesterRequest.OrganizationNumber}";
            var result = await Helpers.HarvestFromSke<ArrearsModel>(evidenceHarvesterRequest, _logger, _client, HttpMethod.Get, url, _settings);

            var dto = new RestanserV2Dto(result);

            var ecb = new EvidenceBuilder(_evidenceSourceMetadata, "RestanserV2");
            ecb.AddEvidenceValue("default", JsonConvert.SerializeObject(dto), Constants.Source, false);
            return ecb.GetEvidenceValues();
        }
    }
}
