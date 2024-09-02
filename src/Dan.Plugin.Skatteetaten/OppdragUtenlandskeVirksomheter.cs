using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var evidenceHarvesterRequest = JsonConvert.DeserializeObject<EvidenceHarvesterRequest>(requestBody);

            return await EvidenceSourceResponse.CreateResponse(req, ()=> GetFromSkeAsync(evidenceHarvesterRequest));
        }


        private async Task<List<EvidenceValue>> GetFromSkeAsync(EvidenceHarvesterRequest req)
        {
            // yes, the 'utenlandskevirksomheter' path needs to be there despite the baseurl already defining it
            // https://app.swaggerhub.com/apis/skatteetaten/oppdrag-utenlandske-virksomheter-api/1.1.0
            var url = $"{_settings.OppdragUtenlandskeVirksomheterEndpoint}/ebevis/utenlandskevirksomheter/{req.OrganizationNumber}/oppdrag/antall";
            dynamic result = await Helpers.HarvestFromSke(req, _logger, _client, HttpMethod.Get, url);

            string orgNo = string.Empty;
            int activeJobs = 0;
            int activeEmployees = 0;
            int registeredJobsAsEmployer = 0;

            // string orgName = result["..."};

            var ecb = new EvidenceBuilder(_evidenceSourceMetadata, "OppdragUtenlandskeVirksomheter");

            try
            {
                if (result["forespurteOrganisasjon"] != null)
                {
                    orgNo = result["forespurteOrganisasjon"];
                    ecb.AddEvidenceValue("organisasjonsnummer", orgNo);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not parse org. no. value : " + orgNo + ". Exception: " + ex.ToString());
            }

            if (result["oppdrag"] != null)
            {
                if (result["oppdrag"]["antallAktiveOppdragSomArbeidsgiver"] != null)
                {
                    try
                    {
                        activeJobs = int.Parse(Convert.ToString(result["oppdrag"]["antallAktiveOppdragSomArbeidsgiver"]));
                        ecb.AddEvidenceValue("antallAktiveOppdragSomArbeidsgiver", activeJobs);
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError("Could not parse activeJobs value : " + activeJobs + ". Exception: " + ex.ToString());
                    }

                }

                if (result["oppdrag"]["antallAktiveArbeidstakere"] != null)
                {
                    try
                    {
                        activeEmployees = int.Parse(Convert.ToString(result["oppdrag"]["antallAktiveArbeidstakere"]));
                        ecb.AddEvidenceValue("antallAktiveArbeidstakere", activeEmployees);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Could not parse antallAktiveArbeidstakere value : " + activeEmployees + ". Exception: " + ex.ToString());
                    }
                }


                if (result["oppdrag"]["antallRegistrerteOppdragSomOppdragsgiver"] != null)
                {
                    try
                    {
                        registeredJobsAsEmployer = int.Parse(Convert.ToString(result["oppdrag"]["antallRegistrerteOppdragSomOppdragsgiver"]));
                        ecb.AddEvidenceValue("antallRegistrerteOppdragSomOppdragsgiver", registeredJobsAsEmployer);
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError("Could not parse antallRegistrerteOppdragSomOppdragsgiver value : " + registeredJobsAsEmployer + ". Exception: " + ex.ToString());
                    }
                }
            }

            return ecb.GetEvidenceValues();
        }
    }
}
