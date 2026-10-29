using System.Diagnostics;
using System.Text.RegularExpressions;
using OpenTelemetry;

namespace Dan.Plugin.Skatteetaten.Utilities
{
    /// <summary>
    /// Masks norwegian national identity numbers (fødselsnummer / d-nummer) in outgoing HTTP dependency spans before they are
    /// exported. The HttpClient instrumentation records the full request URL on the span, and the Freg and SummertSkattegrunnlag
    /// functions put the subject's number in the URL path. The first six digits are kept, matching Party.GetAsString(), so a
    /// request can still be correlated with the application log lines.
    /// </summary>
    public sealed class SsnRedactingProcessor : BaseProcessor<Activity>
    {
        private static readonly string[] UrlTags = { "url.full", "url.path", "http.url", "http.target" };

        private static readonly Regex ElevenDigits = new(@"(?<![0-9])([0-9]{6})[0-9]{5}(?![0-9])", RegexOptions.Compiled);

        public static string Mask(string value)
        {
            return string.IsNullOrEmpty(value) ? value : ElevenDigits.Replace(value, "$1*****");
        }

        public override void OnEnd(Activity activity)
        {
            foreach (var tagName in UrlTags)
            {
                if (activity.GetTagItem(tagName) is string value && ElevenDigits.IsMatch(value))
                {
                    activity.SetTag(tagName, Mask(value));
                }
            }

            if (ElevenDigits.IsMatch(activity.DisplayName))
            {
                activity.DisplayName = Mask(activity.DisplayName);
            }
        }
    }
}
