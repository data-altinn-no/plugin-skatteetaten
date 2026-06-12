using System;
using System.Collections.Generic;
using AwesomeAssertions;
using Dan.Plugin.Skatteetaten.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Dan.Plugin.Skatteetaten.Test
{
    public class FregHendelseslisteModelTests
    {
        // Sample feed payload from the FREG documentation
        // (https://skatteetaten.github.io/folkeregisteret-api-dokumentasjon/hendelsesliste/)
        private const string SampleFeedJson = """
        [
          {
            "sekvensnummer": 1,
            "hendelse": {
              "folkeregisteridentifikator": "69028400470",
              "hendelsetype": "endringIIdentitetsgrunnlag",
              "hendelsesdokument": "55591b51b20518f4f22bf1edd6aa9f25",
              "persondokument": "8f475708c3d855defca884f4af6e49ae",
              "ajourholdstidspunkt": "2017-06-20T09:26:03.689+02:00"
            }
          },
          {
            "sekvensnummer": 2,
            "hendelse": {
              "folkeregisteridentifikator": "69028400470",
              "hendelsetype": "endringIAnnenIdentifikasjon",
              "hendelsesdokument": "1120bea688fb14a292c244592a1aed76",
              "persondokument": "5c3afb4f5afd2fcfbe2f90a6560903a0",
              "ajourholdstidspunkt": "2017-06-20T09:26:03.689+02:00"
            }
          }
        ]
        """;

        [Fact]
        public void Deserializes_feed_array_into_typed_elements()
        {
            var result = JsonConvert.DeserializeObject<List<FregHendelseslisteElement>>(SampleFeedJson);

            result.Should().HaveCount(2);

            var first = result![0];
            first.Sekvensnummer.Should().Be(1);
            first.Hendelse.Folkeregisteridentifikator.Should().Be("69028400470");
            first.Hendelse.Hendelsetype.Should().Be("endringIIdentitetsgrunnlag");
            first.Hendelse.Hendelsesdokument.Should().Be("55591b51b20518f4f22bf1edd6aa9f25");
            first.Hendelse.Persondokument.Should().Be("8f475708c3d855defca884f4af6e49ae");

            result[1].Sekvensnummer.Should().Be(2);
            result[1].Hendelse.Hendelsetype.Should().Be("endringIAnnenIdentifikasjon");
        }

        [Fact]
        public void Preserves_timestamp_offset_when_deserializing()
        {
            var result = JsonConvert.DeserializeObject<List<FregHendelseslisteElement>>(SampleFeedJson);

            result![0].Hendelse.Ajourholdstidspunkt
                .Should().Be(new DateTimeOffset(2017, 6, 20, 9, 26, 3, 689, TimeSpan.FromHours(2)));
        }

        [Fact]
        public void Serializes_back_to_freg_json_field_names()
        {
            var element = new FregHendelseslisteElement
            {
                Sekvensnummer = 42,
                Hendelse = new FregHendelse
                {
                    Folkeregisteridentifikator = "69028400470",
                    Hendelsetype = "endringIIdentitetsgrunnlag",
                    Hendelsesdokument = "abc",
                    Persondokument = "def",
                    Ajourholdstidspunkt = new DateTimeOffset(2017, 6, 20, 9, 26, 3, 689, TimeSpan.FromHours(2))
                }
            };

            var json = JObject.Parse(JsonConvert.SerializeObject(element));

            // The Newtonsoft [JsonProperty] attributes must keep the FREG (lowercase) field names on output.
            json["sekvensnummer"]!.Value<long>().Should().Be(42);
            json["hendelse"]!["folkeregisteridentifikator"]!.Value<string>().Should().Be("69028400470");
            json["hendelse"]!["hendelsetype"]!.Value<string>().Should().Be("endringIIdentitetsgrunnlag");
            json["hendelse"]!["hendelsesdokument"]!.Value<string>().Should().Be("abc");
            json["hendelse"]!["persondokument"]!.Value<string>().Should().Be("def");
            json["hendelse"]!.Value<JObject>().Should().ContainKey("ajourholdstidspunkt");
        }
    }
}
