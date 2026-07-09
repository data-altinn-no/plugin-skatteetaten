using Dan.Plugin.Skatteetaten.Models.Arbeidsgiveravgift;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dan.Plugin.Skatteetaten.Models.Dtos;

[Serializable]
public class ArbeidsgiveravgiftDto
{
    public ArbeidsgiveravgiftDto(PayrollTaxModel model)
    {
        Levert = model.levert;
        ForespurteOrganisasjon = model.forespurteOrganisasjon;
        Arbeidsgiveravgifter = model.arbeidsgiveravgifter?
            .Select(w => new ArbeidsgiveravgiftEntryDto(w.arbeidsgiveravgift))
            .ToList() ?? new List<ArbeidsgiveravgiftEntryDto>();
    }

    [JsonProperty("levert")]
    public DateTime Levert { get; set; }

    [JsonProperty("forespurteOrganisasjon")]
    public string ForespurteOrganisasjon { get; set; }

    [JsonProperty("arbeidsgiveravgifter")]
    public List<ArbeidsgiveravgiftEntryDto> Arbeidsgiveravgifter { get; set; }
}

[Serializable]
public class ArbeidsgiveravgiftEntryDto
{
    public ArbeidsgiveravgiftEntryDto(ArbeidsgiveravgiftEntry entry)
    {
        Termin = entry.termin;
        Aar = entry.aar;
        SumavgiftsgrunnlagBeloep = entry.sumavgiftsgrunnlagBeloep;
    }

    [JsonProperty("termin")]
    public string Termin { get; set; }

    [JsonProperty("aar")]
    public string Aar { get; set; }

    [JsonProperty("sumavgiftsgrunnlagBeloep")]
    public long SumavgiftsgrunnlagBeloep { get; set; }
}
