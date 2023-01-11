using Dan.Common.Exceptions;
using Dan.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dan.Plugin.Skatteetaten.Config;
using Constants = Dan.Plugin.Skatteetaten.Models.Constants;

namespace Dan.Plugin.Skatteetaten.Utilities
{
    public static class Helpers
    {
        public static async Task<dynamic> HarvestFromSke(EvidenceHarvesterRequest req, ILogger logger, HttpClient client, HttpMethod method, string url, ApplicationSettings settings = null)
        {
            logger.LogInformation($"Plugin-skatteetaten: Initiation HarvestFromSke for {req.EvidenceCodeName}:{req.OrganizationNumber} : ");
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(req.JWT))
                request.Headers.TryAddWithoutValidation("AltinnSamtykke", req.JWT);           
            
            if (!string.IsNullOrEmpty(req.MPToken))   
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + req.MPToken);
            else if (settings != null && settings.IsDevEnvironment)
            {
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + GetToken(null, settings.AltinnCertificate));
            }
            var result = await client.SendAsync(request);
            var content = await result.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content) || !result.IsSuccessStatusCode)
            {
                var message = content == string.Empty ? result.ReasonPhrase : content.Trim(new Char[] { '{', '}' }).Replace("\"", "");
                logger.LogWarning("HTTP code from SKE: " + Convert.ToInt32(result.StatusCode).ToString() + " Response: " + message);
                throw new EvidenceSourceTransientException(Constants.ERROR_CCR_UPSTREAM_ERROR, message);
            }

            dynamic poco = JsonConvert.DeserializeObject(content);

            return poco;
        }

        public static async Task<T> HarvestFromSke<T>(EvidenceHarvesterRequest req, ILogger logger, HttpClient client, HttpMethod method, string url, ApplicationSettings settings = null) where T : new()
        {
            var result = await Helpers.HarvestFromSke(req, logger, client, HttpMethod.Get, url, settings);

            var item = JsonConvert.DeserializeObject<T>(result);

            return item;
        }

        //For dev testing purposes
        private static string GetToken(string audience, X509Certificate2 cert)
        {
            var mp = new MaskinportenUtil(audience, "skatteetaten:summertskattegrunnlag", "", false, "https://ver2.maskinporten.no/", cert, "https://ver2.maskinporten.no/", null);
            return mp.GetToken();
        }
    }
}
