using Dan.Common.Exceptions;
using Dan.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dan.Plugin.Skatteetaten.Config;
using Constants = Dan.Plugin.Skatteetaten.Models.Constants;

namespace Dan.Plugin.Skatteetaten.Utilities
{
    public static class Helpers
    {
        private const int MaxLoggedResponseLength = 500;

        public static async Task<dynamic> HarvestFromSke(EvidenceHarvesterRequest req, ILogger logger, HttpClient client, HttpMethod method, string url, ApplicationSettings settings = null)
        {
            var subject = req.SubjectParty?.GetAsString();

            if (settings?.IsDevEnvironment == true)
            {
                logger.LogInformation("Plugin-skatteetaten: HarvestFromSke for {EvidenceCodeName}:{Subject} with consent token present: {HasJwt} and MP token present: {HasMpToken}",
                    req.EvidenceCodeName, subject, !string.IsNullOrEmpty(req.JWT), !string.IsNullOrEmpty(req.MPToken));
            }
            else
            {
                logger.LogInformation("Plugin-skatteetaten: Initiation HarvestFromSke for {EvidenceCodeName}:{Subject}", req.EvidenceCodeName, subject);
            }

            using var request = new HttpRequestMessage(method, url);
            //new consent validation at skatteetaten only requires the consent jw
            if (!string.IsNullOrEmpty(req.JWT))
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + req.JWT);           
            
            if (!string.IsNullOrEmpty(req.MPToken))   
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + req.MPToken);
          
            using var result = await client.SendAsync(request);
            var content = await result.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content) || !result.IsSuccessStatusCode)
            {
                var message = content == string.Empty ? result.ReasonPhrase : content.Trim(new Char[] { '{', '}' }).Replace("\"", "");
                logger.LogWarning("HTTP code from SKE: {StatusCode} Response: {Response}", (int)result.StatusCode, SanitizeForLog(message));
                throw new EvidenceSourceTransientException(Constants.ERROR_CCR_UPSTREAM_ERROR, message);
            }


            //TODO remove
            dynamic poco = JsonConvert.DeserializeObject(content);

            return poco;
        }

        /// <summary>
        /// Masks national identity numbers and caps the length of upstream text before it is written to the logs.
        /// Skatteetaten error responses can echo the requested identifier.
        /// </summary>
        public static string SanitizeForLog(string value)
        {
            var masked = SsnRedactingProcessor.Mask(value);
            return masked?.Length > MaxLoggedResponseLength ? masked.Substring(0, MaxLoggedResponseLength) + "...(truncated)" : masked;
        }

        public static async Task<T> HarvestFromSke<T>(EvidenceHarvesterRequest req, ILogger logger, HttpClient client, HttpMethod method, string url, ApplicationSettings settings = null) where T : new()
        {
            var result = await Helpers.HarvestFromSke(req, logger, client, method, url, settings);

            var item = JsonConvert.DeserializeObject<T>(Convert.ToString(result));

            return item;
        }
    }
}
