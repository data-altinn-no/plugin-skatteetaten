using Dan.Plugin.Skatteetaten.Models.Arrears;
using Newtonsoft.Json;
using System;

namespace Dan.Plugin.Skatteetaten.Models.Dtos;

[Serializable]
public class RestanserV2Dto
{
    public RestanserV2Dto(ArrearsModel model)
    {
        Levert = model.levert;
        ForespurteOrganisasjon = model.forespurteOrganisasjon;
        Restanser = new RestanserDto(model.restanser);
    }

    [JsonProperty("levert")]
    public DateTime Levert { get; set; }

    [JsonProperty("forespurteOrganisasjon")]
    public string ForespurteOrganisasjon { get; set; }

    [JsonProperty("restanser")]
    public RestanserDto Restanser { get; set; }
}

[Serializable]
public class RestanserDto
{
    public RestanserDto(Restanser restanser)
    {
        Arbeidsgiveravgift = new RestanserKategoriDto(restanser.arbeidsgiveravgift);
        Forskuddstrekk = new RestanserKategoriDto(restanser.forskuddstrekk);
        Forskuddsskatt = new RestanserKategoriDto(restanser.forskuddsskatt);
        Restskatt = new RestanserKategoriDto(restanser.restskatt);
        Gebyr = new RestanserKategoriDto(restanser.gebyr);
        Merverdiavgift = new RestanserKategoriDto(restanser.merverdiavgift);
    }

    [JsonProperty("arbeidsgiveravgift")]
    public RestanserKategoriDto Arbeidsgiveravgift { get; set; }

    [JsonProperty("forskuddstrekk")]
    public RestanserKategoriDto Forskuddstrekk { get; set; }

    [JsonProperty("forskuddsskatt")]
    public RestanserKategoriDto Forskuddsskatt { get; set; }

    [JsonProperty("restskatt")]
    public RestanserKategoriDto Restskatt { get; set; }

    [JsonProperty("gebyr")]
    public RestanserKategoriDto Gebyr { get; set; }

    [JsonProperty("merverdiavgift")]
    public RestanserKategoriDto Merverdiavgift { get; set; }
}

[Serializable]
public class RestanserKategoriDto
{
    public RestanserKategoriDto(RestanserKategori kategori)
    {
        ForfaltOgUbetalt = kategori.forfaltOgUbetalt;
        ForfaltOgUbetaltRenter = kategori.forfaltOgUbetaltRenter;
        ForfaltOgUbetaltKrav = kategori.forfaltOgUbetaltKrav;
    }

    [JsonProperty("forfaltOgUbetalt")]
    public decimal ForfaltOgUbetalt { get; set; }

    [JsonProperty("forfaltOgUbetaltRenter")]
    public decimal ForfaltOgUbetaltRenter { get; set; }

    [JsonProperty("forfaltOgUbetaltKrav")]
    public decimal ForfaltOgUbetaltKrav { get; set; }
}
