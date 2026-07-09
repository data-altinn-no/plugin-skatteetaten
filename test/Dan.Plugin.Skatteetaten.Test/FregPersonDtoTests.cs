using AwesomeAssertions;
using Dan.Plugin.Skatteetaten.Models.Dtos;
using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Dan.Plugin.Skatteetaten.Test;

public class FregPersonDtoTests
{
    // Mirrors the settings used in Freg.cs
    private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    //FregPerson endpoint should contain, ident, status, kjonn, fodsel, sivilstand, navn, bostedsadresse, statsborgerskap and status
    private const string SampleFregPersonJson = """
        {
            "identifikasjonsnummer": [
                {
                    "ajourholdstidspunkt": "2020-12-22T16:09:53.4+00:00",
                    "erGjeldende": true,
                    "kilde": "KILDE_DSF",
                    "status": "iBruk",
                    "foedselsEllerDNummer": "29816595620",
                    "identifikatortype": "foedselsnummer"
                }
            ],
            "status":[
                    {
                        "ajourholdstidspunkt": "2020-12-22T16:09:53.4+00:00",
                        "erGjeldende": true,
                        "kilde": "KILDE_DSF",
                        "gyldighetstidspunkt": "2020-12-22T16:09:53.4+00:00",
                        "status": "bosatt"   
                    }
                ],
            "kjoenn": [
                {
                    "erGjeldende": true,
                    "kilde": "KILDE_DSF",
                    "kjoenn": "kvinne"
                }
            ],
            "foedsel": [
                    {
                        "ajourholdstidspunkt": "2022-03-11T08:25:20.86733+00:00",
                        "erGjeldende": true,
                        "kilde": "Synutopia",
                        "gyldighetstidspunkt": "1965-01-29T08:25:20.867293+00:00",
                        "foedselsdato": "1965-01-29",
                        "foedselsaar": "1965",
                        "foedekommuneINorge": "5401",
                        "foedeland": "NOR"
                    }
                ],
            "sivilstand": [
                    {
                        "ajourholdstidspunkt": "2022-03-11T08:25:20.999907+00:00",
                        "erGjeldende": true,
                        "kilde": "Synutopia",
                        "aarsak": "Fødsel",
                        "gyldighetstidspunkt": "1965-01-29T08:25:20.999926+00:00",
                        "sivilstand": "ugift",
                        "sivilstandsdato": "1965-01-29"
                    }
                ],
            "navn": [
                {
                    "ajourholdstidspunkt": "2022-03-11T08:25:20.957496+00:00",
                    "erGjeldende": true,
                    "kilde": "Synutopia",
                    "fornavn": "ALMINNELIG",
                    "etternavn": "AGENT"
                }
            ],
            "bostedsadresse": [
                {
                    "erGjeldende": true,
                    "kilde": "Matrikkelen",
                    "vegadresse": {
                        "kommunenummer": "5001",
                        "adressenavn": "Blaklihøgda",
                        "adressenummer": { "husnummer": "1", "husbokstav": "A" },
                        "poststed": { "postnummer": "7036", "poststedsnavn": "TRONDHEIM" }
                    }
                }                
            ],
            "statsborgerskap": [
                {
                    "aarsak":  "FÃ¸dsel",
                    "ajourholdstidspunkt":  "2022-03-11T09:25:21.045163+01:00",
                    "erGjeldende":  true,
                    "ervervsdato":  "1965-01-29",
                    "gyldighetstidspunkt":  "1965-01-29T09:25:21.045197+01:00",
                    "kilde":  "Synutopia",
                    "statsborgerskap":  "NOR"
                }
            ]
        }
        """;

    //FregPersonRealasjonUtvidet endpoint should contain ident and familierelasjon
    private const string SampleFregPersonRelasjonUtvidetJson = """
        {
            "familierelasjon": [
                {
                    "erGjeldende": true,
                    "kilde": "Synutopia",
                    "minRolleForPerson": "mor",
                    "relatertPerson": "12345678901",
                    "relatertPersonsRolle": "barn"
                }
            ],
            "identifikasjonsnummer": [
            {
                "ajourholdstidspunkt": "2020-12-22T16:09:53.4+00:00",
                "erGjeldende": true,
                "kilde": "KILDE_DSF",
                "status": "iBruk",
                "foedselsEllerDNummer": "29816595620",
                "identifikatortype": "foedselsnummer"
            }
        ]   
        }
        """;

    // FregPersonUtenHjemmel endpoint should contain, ident, status, kjonn, fodsel, sivilstand, navn, bostedsadresse, statsborgerskap
    private const string SampleFregPersonUtenHjemmelJson = """
        {
            "identifikasjonsnummer": [
                {
                    "ajourholdstidspunkt": "2020-12-22T16:09:53.4+00:00",
                    "erGjeldende": true,
                    "kilde": "KILDE_DSF",
                    "status": "iBruk",
                    "foedselsEllerDNummer": "29816595620",
                    "identifikatortype": "foedselsnummer"
                }
            ],
            "status": [
                {
                    "ajourholdstidspunkt": "2020-12-22T16:09:53.4+00:00",
                    "erGjeldende": true,
                    "gyldighetstidspunkt": "2020-12-22T16:09:53.4+00:00",
                    "status": "bosatt"
                }
            ],
            "kjoenn": [
                {
                    "erGjeldende": true,
                    "kilde": "KILDE_DSF",
                    "kjoenn": "kvinne"
                }
            ],
            "foedsel": [
                {
                    "ajourholdstidspunkt": "2022-03-11T08:25:20.86733+00:00",
                    "erGjeldende": true,
                    "gyldighetstidspunkt": "1965-01-29T08:25:20.867293+00:00",
                    "foedselsdato": "1965-01-29",
                    "foedselsaar": "1965",
                    "foedekommuneINorge": "5401",
                    "foedeland": "NOR"
                }
            ],
            "sivilstand": [
                {
                    "erGjeldende": true,
                    "sivilstand": "ugift"
                }
            ],
            "navn": [
                {
                    "ajourholdstidspunkt": "2022-03-11T08:25:20.957496+00:00",
                    "erGjeldende": true,
                    "kilde": "Synutopia",
                    "fornavn": "ALMINNELIG",
                    "etternavn": "AGENT"
                }
            ],
            "bostedsadresse": [
                {
                    "erGjeldende": true,
                    "kilde": "Matrikkelen",
                    "vegadresse": {
                        "kommunenummer": "5001",
                        "adressenavn": "Blaklihøgda",
                        "adressenummer": { "husnummer": "1", "husbokstav": "A" },
                        "poststed": { "postnummer": "7036", "poststedsnavn": "TRONDHEIM" }
                    }
                }
            ],
            "statsborgerskap": [
                {
                    "erGjeldende": true,
                    "statsborgerskap": "NOR"
                }
            ]
        }
        """;

    [Fact]
    public void FregPerson_serialization_contains_correct_fields()
    {
        var dto = JsonConvert.DeserializeObject<FregPersonDto>(SampleFregPersonJson);
        var json = JObject.Parse(JsonConvert.SerializeObject(dto, JsonSettings));

        // Populated parts are present
        json.Should().ContainKey("identifikasjonsnummer");
        json.Should().ContainKey("kjoenn");
        json.Should().ContainKey("navn");
        json.Should().ContainKey("bostedsadresse");
        json.Should().ContainKey("foedsel");
        json.Should().ContainKey("sivilstand");
        json.Should().ContainKey("statsborgerskap");
        json.Should().ContainKey("status");
    }

    [Fact]
    public void FregPersonRelasjonUtvidet_contains_correct_fields()
    {
        var dto = JsonConvert.DeserializeObject<FregPersonDto>(SampleFregPersonRelasjonUtvidetJson);
        var json = JObject.Parse(JsonConvert.SerializeObject(dto, JsonSettings));

        // Populated parts are present
        json.Should().ContainKey("familierelasjon");
        json.Should().ContainKey("identifikasjonsnummer");
        json.Should().NotContainKey("status");
        json.Should().NotContainKey("kjoenn");
        json.Should().NotContainKey("foedsel");
        json.Should().NotContainKey("sivilstand");
        json.Should().NotContainKey("navn");
        json.Should().NotContainKey("bostedsadresse");
        json.Should().NotContainKey("statsborgerskap");
    }

    [Fact]
    public void FregPersonUtenHjemmel_contains_correct_fields()
    {
        // FregPersonUtenHjemmel and FregPerson contains the same properties
        var dto = JsonConvert.DeserializeObject<FregPersonDto>(SampleFregPersonUtenHjemmelJson);
        var json = JObject.Parse(JsonConvert.SerializeObject(dto, JsonSettings));

        json.Should().ContainKey("identifikasjonsnummer");       
        json.Should().ContainKey("status");       
        json.Should().ContainKey("kjoenn");       
        json.Should().ContainKey("foedsel");       
        json.Should().ContainKey("sivilstand");       
        json.Should().ContainKey("navn");       
        json.Should().ContainKey("bostedsadresse");       
        json.Should().ContainKey("statsborgerskap");       
    }
}
