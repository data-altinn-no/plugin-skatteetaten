using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Models;
using Dan.Plugin.Skatteetaten.Models.Dtos;
using Dan.Plugin.Skatteetaten.Models.OppdragUtenlandskeVirksomheter;
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
    public class OppdragUtenlandskeVirksomheter
    {
        private HttpClient _client;
        private ApplicationSettings _settings;
        private readonly ILogger _logger;
        private IEvidenceSourceMetadata _evidenceSourceMetadata;

        public OppdragUtenlandskeVirksomheter(IHttpClientFactory factory, IOptions<ApplicationSettings> settings, ILoggerFactory loggerFactory,
            IEvidenceSourceMetadata metadata)
        {
            _client = factory.CreateClient(DanConstants.SafeHttpClient);
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<OppdragUtenlandskeVirksomheter>();
            _evidenceSourceMetadata = metadata;
        }

        [Function("OppdragUtenlandskeVirksomheter")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();
            return await EvidenceSourceResponse.CreateResponse(req, () => GetFromSkeAsync(evidenceHarvesterRequest));
        }

        private async Task<List<EvidenceValue>> GetFromSkeAsync(EvidenceHarvesterRequest req)
        {
            // yes, the 'utenlandskevirksomheter' path needs to be there despite the baseurl already defining it
            // https://app.swaggerhub.com/apis/skatteetaten/oppdrag-utenlandske-virksomheter-api/1.1.0
            var url = $"{_settings.OppdragUtenlandskeVirksomheterEndpoint}/v1/ebevis/utenlandskevirksomheter/{req.OrganizationNumber}/oppdrag/antall";
            var result = await Helpers.HarvestFromSke<OppdragUtenlandskeVirksomheterModel>(req, _logger, _client, HttpMethod.Get, url, _settings);

            var dto = new OppdragUtenlandskeVirksomheterDto(result);

            var ecb = new EvidenceBuilder(_evidenceSourceMetadata, "OppdragUtenlandskeVirksomheter");
            ecb.AddEvidenceValue("default", JsonConvert.SerializeObject(dto), Constants.Source, false);
            return ecb.GetEvidenceValues();
        }
    }
}
