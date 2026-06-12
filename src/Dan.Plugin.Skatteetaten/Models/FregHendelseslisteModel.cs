using System;
using Newtonsoft.Json;

namespace Dan.Plugin.Skatteetaten.Models
{
    /// <summary>
    /// One entry in the folkeregisteret hendelsesliste feed (GET /v1/hendelser/feed).
    /// The feed is returned as a JSON array of these elements, paginated by sekvensnummer.
    /// </summary>
    public class FregHendelseslisteElement
    {
        [JsonProperty("sekvensnummer")]
        public long Sekvensnummer { get; set; }

        [JsonProperty("hendelse")]
        public FregHendelse Hendelse { get; set; }
    }

    public class FregHendelse
    {
        [JsonProperty("folkeregisteridentifikator")]
        public string Folkeregisteridentifikator { get; set; }

        [JsonProperty("hendelsetype")]
        public string Hendelsetype { get; set; }

        [JsonProperty("hendelsesdokument")]
        public string Hendelsesdokument { get; set; }

        [JsonProperty("persondokument")]
        public string Persondokument { get; set; }

        [JsonProperty("ajourholdstidspunkt")]
        public DateTimeOffset Ajourholdstidspunkt { get; set; }
    }
}
