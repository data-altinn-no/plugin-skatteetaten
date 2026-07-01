using Dan.Plugin.Skatteetaten.Models.OppdragUtenlandskeVirksomheter;
using Newtonsoft.Json;
using System;

namespace Dan.Plugin.Skatteetaten.Models.Dtos;

[Serializable]
public class OppdragUtenlandskeVirksomheterDto
{
    public OppdragUtenlandskeVirksomheterDto(OppdragUtenlandskeVirksomheterModel model)
    {
        ForespurteOrganisasjon = model.forespurteOrganisasjon;
        Oppdrag = new OppdragAntallDto(model.oppdrag);
    }

    [JsonProperty("forespurteOrganisasjon")]
    public string ForespurteOrganisasjon { get; set; }

    [JsonProperty("oppdrag")]
    public OppdragAntallDto Oppdrag { get; set; }
}

[Serializable]
public class OppdragAntallDto
{
    public OppdragAntallDto(OppdragAntall oppdrag)
    {
        AntallAktiveOppdragSomArbeidsgiver = oppdrag.antallAktiveOppdragSomArbeidsgiver;
        AntallAktiveArbeidstakere = oppdrag.antallAktiveArbeidstakere;
        AntallRegistrerteOppdragSomOppdragsgiver = oppdrag.antallRegistrerteOppdragSomOppdragsgiver;
    }

    [JsonProperty("antallAktiveOppdragSomArbeidsgiver")]
    public int AntallAktiveOppdragSomArbeidsgiver { get; set; }

    [JsonProperty("antallAktiveArbeidstakere")]
    public int AntallAktiveArbeidstakere { get; set; }

    [JsonProperty("antallRegistrerteOppdragSomOppdragsgiver")]
    public int AntallRegistrerteOppdragSomOppdragsgiver { get; set; }
}
