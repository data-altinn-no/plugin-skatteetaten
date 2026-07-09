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
        Restanser = model.restanser != null ? new RestanserDto(model.restanser) : null;
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
        Arbeidsgiveravgift = restanser.arbeidsgiveravgift != null ? new RestanserKategoriDto(restanser.arbeidsgiveravgift) : null;
        Forskuddstrekk = restanser.forskuddstrekk != null ? new RestanserKategoriDto(restanser.forskuddstrekk) : null;
        Forskuddsskatt = restanser.forskuddsskatt != null ? new RestanserKategoriDto(restanser.forskuddsskatt) : null;
        Restskatt = restanser.restskatt != null ? new RestanserKategoriDto(restanser.restskatt) : null;
        Gebyr = restanser.gebyr != null ? new RestanserKategoriDto(restanser.gebyr) : null;
        Merverdiavgift = restanser.merverdiavgift != null ? new RestanserKategoriDto(restanser.merverdiavgift) : null;
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
