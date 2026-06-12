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

        private const string ASA_UH = "Altinn Studio-appsUH";


        private List<KeyValuePair<string, string>> serviceContextRightsPkg = new List<KeyValuePair<string, string>>();

        //servicecontextname, feed url - the hendelsesliste feed is not tied to a single person, so it has its own mapping
        private List<KeyValuePair<string, string>> serviceContextFeedPkg = new List<KeyValuePair<string, string>>();

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
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("OED", $"{ENV}folkeregisteret/offentlig-med-hjemmel/api/v1/personer/{PERSON}?part={PART}"));
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("Altinn Studio-apps", $"{ENV}folkeregisteret/offentlig-med-hjemmel/api/v1/personer/{PERSON}?part={PART}"));
            serviceContextRightsPkg.Add(new KeyValuePair<string, string>("Altinn Studio-appsUH", $"{ENV}folkeregisteret/api/offentligutenhjemmel/v1/personer/{PERSON}?part={PART}"));

            //servicecontextname, url for the hendelsesliste feed (/v1/hendelser/feed)
            serviceContextFeedPkg.Add(new KeyValuePair<string, string>("OED", $"{ENV}folkeregisteret/offentlig-med-hjemmel/api/v1/hendelser/feed/"));
            serviceContextFeedPkg.Add(new KeyValuePair<string, string>("Altinn Studio-apps", $"{ENV}folkeregisteret/offentlig-med-hjemmel/api/v1/hendelser/feed/"));
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

        private string GetFeedUrlForServiceContext(string serviceContext, string sekvensnummer)
        {
            var kvp = serviceContextFeedPkg.Where(x => x.Key == serviceContext).FirstOrDefault();

            if (string.IsNullOrEmpty(kvp.Value))
            {
                _logger.LogError($"FregHendelsesliste: feed not defined for {serviceContext}");
                throw new EvidenceSourcePermanentServerException(Constants.ERROR_CCR_UPSTREAM_ERROR, "No feed endpoint available for servicecontext");
            }

            if (string.IsNullOrEmpty(sekvensnummer))
            {
                _logger.LogError("FregHendelsesliste: sekvensnummer not supplied");
                throw new EvidenceSourcePermanentClientException(Constants.ERROR_CCR_UPSTREAM_ERROR, "Required parameter 'sekvensnummer' is missing");
            }

            var url = kvp.Value.Replace(ENV, _settings.FregEnvironment);

            //the feed is paginated by sekvensnummer - the consumer keeps an internal pointer and passes it back as the start of the next page
            url += $"?seq={sekvensnummer}";

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

        [Function("FregPersonUtenHjemmel")]
        public async Task<HttpResponseData> FregPersonUtenHjemmel([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            var url = GetUrlForServiceContext(evidenceHarvesterRequest.SubjectParty.GetAsString(false), ASA_UH, evidenceHarvesterRequest.TryGetParameter("part", out string partParam) ? partParam : PartPersonBasis, evidenceHarvesterRequest.TryGetParameter("parts", out string partsParam) ? partsParam : string.Empty);

            return await EvidenceSourceResponse.CreateResponse(req, () => GetFregPerson(evidenceHarvesterRequest, url));
        }

        [Function("FregHendelsesliste")]
        public async Task<HttpResponseData> FregHendelsesliste([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, FunctionContext context)
        {
            var evidenceHarvesterRequest = await req.ReadFromJsonAsync<EvidenceHarvesterRequest>();

            var url = GetFeedUrlForServiceContext(evidenceHarvesterRequest.ServiceContext, evidenceHarvesterRequest.TryGetParameter("sekvensnummer", out string sekvensnummerParam) ? sekvensnummerParam : string.Empty);

            return await EvidenceSourceResponse.CreateResponse(req, () => GetFregHendelsesliste(evidenceHarvesterRequest, url));
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

        private async Task<List<EvidenceValue>> GetFregHendelsesliste(EvidenceHarvesterRequest req, string url)
        {
            var result = await Helpers.HarvestFromSke<List<FregHendelseslisteElement>>(req, _logger, _client, HttpMethod.Get, url);

            var ecb = new EvidenceBuilder(_metadata, "FregHendelsesliste");
            ecb.AddEvidenceValue("default", JsonConvert.SerializeObject(result), "Skatteetaten", false);

            return ecb.GetEvidenceValues();
        }
    }
}
