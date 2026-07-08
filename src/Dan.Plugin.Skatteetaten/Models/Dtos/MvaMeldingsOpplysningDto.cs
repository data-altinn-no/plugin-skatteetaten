using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dan.Plugin.Skatteetaten.Models.Dtos;

[Serializable]
public class MvaMeldingsOpplysningDto
{
    public MvaMeldingsOpplysningDto(VATReportModel model)
    {
        Levert = model.levert;
        ForespurteOrganisasjon = model.forespurteOrganisasjon;
        MvaAlminneligNaering = model.mvaAlminneligNaering != null ? new MvaAlminneligNaeringDto(model.mvaAlminneligNaering) : null;
    }

    [JsonProperty("levert")]
    public DateTime Levert { get; set; }

    [JsonProperty("forespurteOrganisasjon")]
    public string ForespurteOrganisasjon { get; set; }

    [JsonProperty("mvaAlminneligNaering")]
    public MvaAlminneligNaeringDto MvaAlminneligNaering { get; set; }
}

[Serializable]
public class MvaAlminneligNaeringDto
{
    public MvaAlminneligNaeringDto(MvaAlminneligNaering model)
    {
        Skattemeldingsplikt = model.skattemeldingsplikt != null ? new SkattemeldingspliktDto(model.skattemeldingsplikt) : null;
        AnsvarligForMvaMelding = model.ansvarligForMvaMelding != null ? new AnsvarligForMvaMeldingDto(model.ansvarligForMvaMelding) : null;
        SamletFastsattOgReskontrofoertForTermin = model.samletFastsattOgReskontrofoertForTermin?
            .Select(t => new SamletFastsattDto(t))
            .ToList() ?? new List<SamletFastsattDto>();
    }

    [JsonProperty("skattemeldingsplikt")]
    public SkattemeldingspliktDto Skattemeldingsplikt { get; set; }

    [JsonProperty("ansvarligForMvaMelding")]
    public AnsvarligForMvaMeldingDto AnsvarligForMvaMelding { get; set; }

    [JsonProperty("samletFastsattOgReskontrofoertForTermin")]
    public List<SamletFastsattDto> SamletFastsattOgReskontrofoertForTermin { get; set; }
}

[Serializable]
public class SkattemeldingspliktDto
{
    public SkattemeldingspliktDto(Skattemeldingsplikt model)
    {
        Termintype = model.termintype;
        FoersteTermin = model.foersteTermin != null ? new TerminDto(model.foersteTermin.termin, model.foersteTermin.aar) : null;
        SisteTermin = model.sisteTermin != null ? new TerminDto(model.sisteTermin.termin, model.sisteTermin.aar) : null;
    }

    [JsonProperty("termintype")]
    public string Termintype { get; set; }

    [JsonProperty("foersteTermin")]
    public TerminDto FoersteTermin { get; set; }

    [JsonProperty("sisteTermin")]
    public TerminDto SisteTermin { get; set; }
}

[Serializable]
public class TerminDto
{
    public TerminDto(string termin, string aar)
    {
        Termin = termin;
        Aar = aar;
    }

    [JsonProperty("termin")]
    public string Termin { get; set; }

    [JsonProperty("aar")]
    public string Aar { get; set; }
}

[Serializable]
public class AnsvarligForMvaMeldingDto
{
    public AnsvarligForMvaMeldingDto(AnsvarligForMvaMelding model)
    {
        Organisasjonsnummer = model.organisasjonsnummer;
        Organisasjonsnavn = model.oragnisasjonsnavn;
    }

    [JsonProperty("organisasjonsnummer")]
    public int Organisasjonsnummer { get; set; }

    [JsonProperty("organisasjonsnavn")]
    public string Organisasjonsnavn { get; set; }
}

[Serializable]
public class SamletFastsattDto
{
    public SamletFastsattDto(SamletFastsattOgReskontrofoertForTermin model)
    {
        GjelderTermin = model.gjelderTermin != null ? new TerminDto(model.gjelderTermin.termin, model.gjelderTermin.aar) : null;
        FastsettingsperiodeStatus = model.fastsettingsperiodeStatus;
        ErMyndighetsfastsatt = model.erMyndighetsfastsatt;
        GrunnMyndighetsfastsatt = model.grunnMyndighetsfastsatt;
        MvaAvgift = model.mvaAvgift != null ? new MvaAvgiftDto(model.mvaAvgift) : null;
        MvaGrunnlag = model.mvaGrunnlag != null ? new MvaGrunnlagDto(model.mvaGrunnlag) : null;
    }

    [JsonProperty("gjelderTermin")]
    public TerminDto GjelderTermin { get; set; }

    [JsonProperty("fastsettingsperiodeStatus")]
    public string FastsettingsperiodeStatus { get; set; }

    [JsonProperty("erMyndighetsfastsatt")]
    public bool? ErMyndighetsfastsatt { get; set; }

    [JsonProperty("grunnMyndighetsfastsatt")]
    public string GrunnMyndighetsfastsatt { get; set; }

    [JsonProperty("mvaAvgift")]
    public MvaAvgiftDto MvaAvgift { get; set; }

    [JsonProperty("mvaGrunnlag")]
    public MvaGrunnlagDto MvaGrunnlag { get; set; }
}

[Serializable]
public class MvaAvgiftDto
{
    public MvaAvgiftDto(MvaAvgift model)
    {
        InnlandOmsetningUttakHoeySats = model.innlandOmsetningUttakHoeySats;
        InnlandOmsetningUttakMiddelsSats = model.innlandOmsetningUttakMiddelsSats;
        InnlandOmsetningUttakLavSats = model.innlandOmsetningUttakLavSats;
        FradragInnlandInngaaendeHoeySats = model.fradragInnlandInngaaendeHoeySats;
        FradragInnlandInngaaendeMiddelsSats = model.fradragInnlandInngaaendeMiddelsSats;
        FradragInnlandInngaaendeLavSats = model.fradragInnlandInngaaendeLavSats;
    }

    [JsonProperty("innlandOmsetningUttakHoeySats")]
    public int InnlandOmsetningUttakHoeySats { get; set; }

    [JsonProperty("innlandOmsetningUttakMiddelsSats")]
    public int InnlandOmsetningUttakMiddelsSats { get; set; }

    [JsonProperty("innlandOmsetningUttakLavSats")]
    public int InnlandOmsetningUttakLavSats { get; set; }

    [JsonProperty("fradragInnlandInngaaendeHoeySats")]
    public int FradragInnlandInngaaendeHoeySats { get; set; }

    [JsonProperty("fradragInnlandInngaaendeMiddelsSats")]
    public int FradragInnlandInngaaendeMiddelsSats { get; set; }

    [JsonProperty("fradragInnlandInngaaendeLavSats")]
    public int FradragInnlandInngaaendeLavSats { get; set; }
}

[Serializable]
public class MvaGrunnlagDto
{
    public MvaGrunnlagDto(MvaGrunnlag model)
    {
        InnlandOmsetningUttakHoeySats = model.innlandOmsetningUttakHoeySats;
        InnlandOmsetningUttakMiddelsSats = model.innlandOmsetningUttakMiddelsSats;
        InnlandOmsetningUttakLavSats = model.innlandOmsetningUttakLavSats;
    }

    [JsonProperty("innlandOmsetningUttakHoeySats")]
    public int InnlandOmsetningUttakHoeySats { get; set; }

    [JsonProperty("innlandOmsetningUttakMiddelsSats")]
    public int InnlandOmsetningUttakMiddelsSats { get; set; }

    [JsonProperty("innlandOmsetningUttakLavSats")]
    public int InnlandOmsetningUttakLavSats { get; set; }
}
