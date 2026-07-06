using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dan.Plugin.Skatteetaten.Models.Dtos;

// ── Root ─────────────────────────────────────────────────────────────────────

[Serializable]
public class FregPersonDto
{
    [JsonProperty("adressebeskyttelse")]
    public List<AdressebeskyttelseDto> Adressebeskyttelse { get; set; }

    [JsonProperty("bibehold")]
    public List<BibeholdDto> Bibehold { get; set; }

    [JsonProperty("bostedsadresse")]
    public List<BostedsadresseDto> Bostedsadresse { get; set; }

    [JsonProperty("brukAvSamiskSpraak")]
    public List<BrukAvSamiskSpraakDto> BrukAvSamiskSpraak { get; set; }

    [JsonProperty("deltBosted")]
    public List<DeltBostedDto> DeltBosted { get; set; }

    [JsonProperty("doedsfall")]
    public DoedsfallDto Doedsfall { get; set; }

    [JsonProperty("falskIdentitet")]
    public FalskIdentitetDto FalskIdentitet { get; set; }

    [JsonProperty("familierelasjon")]
    public List<FamilierelasjonsDto> Familierelasjon { get; set; }

    [JsonProperty("foedsel")]
    public List<FoedselDto> Foedsel { get; set; }

    [JsonProperty("foedselINorge")]
    public List<FoedselINorgeDto> FoedselINorge { get; set; }

    [JsonProperty("foreldreansvar")]
    public List<ForeldreansvarDto> Foreldreansvar { get; set; }

    [JsonProperty("forholdTilSametingetsValgmanntall")]
    public List<ForholdTilSametingetsValgmanntallDto> ForholdTilSametingetsValgmanntall { get; set; }

    [JsonProperty("fratattRettsligHandleevne")]
    public List<FratattRettsligHandleevneDto> FratattRettsligHandleevne { get; set; }

    [JsonProperty("identifikasjonsnummer")]
    public List<IdentifikasjonsnummerDto> Identifikasjonsnummer { get; set; }

    [JsonProperty("identitetsgrunnlag")]
    public List<IdentitetsgrunnlagDto> Identitetsgrunnlag { get; set; }

    [JsonProperty("innflytting")]
    public List<InnflyttingDto> Innflytting { get; set; }

    [JsonProperty("kjoenn")]
    public List<KjoennDto> Kjoenn { get; set; }

    [JsonProperty("kontaktinformasjonForDoedsbo")]
    public List<KontaktinformasjonForDoedsboDto> KontaktinformasjonForDoedsbo { get; set; }

    [JsonProperty("legitimasjonsdokument")]
    public List<LegitimasjonsdokumentDto> Legitimasjonsdokument { get; set; }

    [JsonProperty("navn")]
    public List<NavnDto> Navn { get; set; }

    [JsonProperty("opphold")]
    public List<OppholdDto> Opphold { get; set; }

    [JsonProperty("oppholdPaaSvalbard")]
    public List<OppholdPaaSvalbardDto> OppholdPaaSvalbard { get; set; }

    [JsonProperty("oppholdsadresse")]
    public List<OppholdsadresseDto> Oppholdsadresse { get; set; }

    [JsonProperty("postadresse")]
    public List<PostadresseDto> Postadresse { get; set; }

    [JsonProperty("postadresseIUtlandet")]
    public List<PostadresseIUtlandetDto> PostadresseIUtlandet { get; set; }

    [JsonProperty("preferertKontaktadresse")]
    public List<PreferertKontaktadresseDto> PreferertKontaktadresse { get; set; }

    [JsonProperty("rettsligHandleevne")]
    public List<RettsligHandleevneDto> RettsligHandleevne { get; set; }

    [JsonProperty("sivilstand")]
    public List<SivilstandDto> Sivilstand { get; set; }

    [JsonProperty("statsborgerskap")]
    public List<StatsborgerskapDto> Statsborgerskap { get; set; }

    [JsonProperty("status")]
    public List<StatusDto> Status { get; set; }

    [JsonProperty("utenlandskPersonidentifikasjon")]
    public List<UtenlandskPersonidentifikasjonDto> UtenlandskPersonidentifikasjon { get; set; }

    [JsonProperty("utflytting")]
    public List<UtflyttingDto> Utflytting { get; set; }

    [JsonProperty("utlendingsmyndighetenesIdentifikasjonsnummer")]
    public List<UtlendingsmyndighetenesIdentifikasjonsnummerDto> UtlendingsmyndighetenesIdentifikasjonsnummer { get; set; }

    [JsonProperty("vergemaalEllerFremtidsfullmakt")]
    public List<VergemaalEllerFremtidsfullmaktDto> VergemaalEllerFremtidsfullmakt { get; set; }
}

// ── Shared sub-types ─────────────────────────────────────────────────────────

[Serializable]
public class FregPersonnavnDto
{
    [JsonProperty("etternavn")]
    public string Etternavn { get; set; }

    [JsonProperty("fornavn")]
    public string Fornavn { get; set; }

    [JsonProperty("mellomnavn")]
    public string Mellomnavn { get; set; }
}

[Serializable]
public class FregPoststedDto
{
    [JsonProperty("postnummer")]
    public string Postnummer { get; set; }

    [JsonProperty("poststedsnavn")]
    public string Poststedsnavn { get; set; }
}

[Serializable]
public class FregAdressenummerDto
{
    [JsonProperty("husbokstav")]
    public string Husbokstav { get; set; }

    [JsonProperty("husnummer")]
    public string Husnummer { get; set; }
}

[Serializable]
public class FregMatrikkelnummerDto
{
    [JsonProperty("bruksnummer")]
    public int Bruksnummer { get; set; }

    [JsonProperty("festenummer")]
    public int Festenummer { get; set; }

    [JsonProperty("gaardsnummer")]
    public int Gaardsnummer { get; set; }

    [JsonProperty("kommunenummer")]
    public string Kommunenummer { get; set; }
}

[Serializable]
public class FregMatrikkeladresseDto
{
    [JsonProperty("adressetilleggsnavn")]
    public string Adressetilleggsnavn { get; set; }

    [JsonProperty("bruksenhetsnummer")]
    public string Bruksenhetsnummer { get; set; }

    [JsonProperty("bruksenhetstype")]
    public string Bruksenhetstype { get; set; }

    [JsonProperty("coAdressenavn")]
    public string CoAdressenavn { get; set; }

    [JsonProperty("matrikkelnummer")]
    public FregMatrikkelnummerDto Matrikkelnummer { get; set; }

    [JsonProperty("poststed")]
    public FregPoststedDto Poststed { get; set; }

    [JsonProperty("undernummer")]
    public int? Undernummer { get; set; }
}

[Serializable]
public class FregVegadresseDto
{
    [JsonProperty("adressekode")]
    public string Adressekode { get; set; }

    [JsonProperty("adressenavn")]
    public string Adressenavn { get; set; }

    [JsonProperty("adressenummer")]
    public FregAdressenummerDto Adressenummer { get; set; }

    [JsonProperty("adressetilleggsnavn")]
    public string Adressetilleggsnavn { get; set; }

    [JsonProperty("bruksenhetsnummer")]
    public string Bruksenhetsnummer { get; set; }

    [JsonProperty("bruksenhetstype")]
    public string Bruksenhetstype { get; set; }

    [JsonProperty("coAdressenavn")]
    public string CoAdressenavn { get; set; }

    [JsonProperty("kommunenummer")]
    public string Kommunenummer { get; set; }

    [JsonProperty("poststed")]
    public FregPoststedDto Poststed { get; set; }
}

[Serializable]
public class FregUkjentBostedDto
{
    [JsonProperty("bostedskommune")]
    public string Bostedskommune { get; set; }
}

[Serializable]
public class FregUtenlandskAdresseDto
{
    [JsonProperty("adressenavn")]
    public string Adressenavn { get; set; }

    [JsonProperty("boenhet")]
    public string Boenhet { get; set; }

    [JsonProperty("byEllerStedsnavn")]
    public string ByEllerStedsnavn { get; set; }

    [JsonProperty("bygning")]
    public string Bygning { get; set; }

    [JsonProperty("coAdressenavn")]
    public string CoAdressenavn { get; set; }

    [JsonProperty("distriktsnavn")]
    public string Distriktsnavn { get; set; }

    [JsonProperty("etasjenummer")]
    public string Etasjenummer { get; set; }

    [JsonProperty("landkode")]
    public string Landkode { get; set; }

    [JsonProperty("postboks")]
    public string Postboks { get; set; }

    [JsonProperty("postkode")]
    public string Postkode { get; set; }

    [JsonProperty("region")]
    public string Region { get; set; }
}

// Person without folkeregister identifier — two variants (one uses "navn", one uses "personnavn")
[Serializable]
public class FregPersonUtenIdNavnDto
{
    [JsonProperty("foedselsdato")]
    public string Foedselsdato { get; set; }

    [JsonProperty("kjoenn")]
    public string Kjoenn { get; set; }

    [JsonProperty("navn")]
    public FregPersonnavnDto Navn { get; set; }

    [JsonProperty("statsborgerskap")]
    public string Statsborgerskap { get; set; }
}

[Serializable]
public class FregPersonUtenIdPersonnavnDto
{
    [JsonProperty("foedselsdato")]
    public string Foedselsdato { get; set; }

    [JsonProperty("kjoenn")]
    public string Kjoenn { get; set; }

    [JsonProperty("personnavn")]
    public FregPersonnavnDto Personnavn { get; set; }

    [JsonProperty("statsborgerskap")]
    public List<string> Statsborgerskap { get; set; }
}

// ── Part: adressebeskyttelse ─────────────────────────────────────────────────

[Serializable]
public class AdressebeskyttelseDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("graderingsnivaa")]
    public string Graderingsnivaa { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: bibehold ───────────────────────────────────────────────────────────

[Serializable]
public class BibeholdDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("bibeholdstatus")]
    public string Bibeholdstatus { get; set; }

    [JsonProperty("datoForBibehold")]
    public string DatoForBibehold { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: bostedsadresse ─────────────────────────────────────────────────────

[Serializable]
public class BostedsadresseDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("adresseIdentifikatorFraMatrikkelen")]
    public string AdresseIdentifikatorFraMatrikkelen { get; set; }

    [JsonProperty("adressegradering")]
    public string Adressegradering { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("flyttedato")]
    public string Flyttedato { get; set; }

    [JsonProperty("grunnkrets")]
    public int? Grunnkrets { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("kirkekrets")]
    public int? Kirkekrets { get; set; }

    [JsonProperty("matrikkeladresse")]
    public FregMatrikkeladresseDto Matrikkeladresse { get; set; }

    [JsonProperty("naerAdresseIdentifikatorFraMatrikkelen")]
    public string NaerAdresseIdentifikatorFraMatrikkelen { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("skolekrets")]
    public int? Skolekrets { get; set; }

    [JsonProperty("stemmekrets")]
    public int? Stemmekrets { get; set; }

    [JsonProperty("ukjentBosted")]
    public FregUkjentBostedDto UkjentBosted { get; set; }

    [JsonProperty("vegadresse")]
    public FregVegadresseDto Vegadresse { get; set; }
}

// ── Part: brukAvSamiskSpraak ─────────────────────────────────────────────────

[Serializable]
public class BrukAvSamiskSpraakDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("spraak")]
    public List<string> Spraak { get; set; }
}

// ── Part: deltBosted ─────────────────────────────────────────────────────────

[Serializable]
public class DeltBostedDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("adresseIdentifikatorFraMatrikkelen")]
    public string AdresseIdentifikatorFraMatrikkelen { get; set; }

    [JsonProperty("adressegradering")]
    public string Adressegradering { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("matrikkeladresse")]
    public FregMatrikkeladresseDto Matrikkeladresse { get; set; }

    [JsonProperty("naerAdresseIdentifikatorFraMatrikkelen")]
    public string NaerAdresseIdentifikatorFraMatrikkelen { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("sluttdatoForKontrakt")]
    public string SluttdatoForKontrakt { get; set; }

    [JsonProperty("startdatoForKontrakt")]
    public string StartdatoForKontrakt { get; set; }

    [JsonProperty("ukjentBosted")]
    public FregUkjentBostedDto UkjentBosted { get; set; }

    [JsonProperty("vegadresse")]
    public FregVegadresseDto Vegadresse { get; set; }
}

// ── Part: doedsfall ──────────────────────────────────────────────────────────

[Serializable]
public class DoedsfallDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("doedsdato")]
    public string Doedsdato { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: falskIdentitet ─────────────────────────────────────────────────────

[Serializable]
public class FalskIdentitetRettIdentitetVedOpplysningerDto
{
    [JsonProperty("foedselsdato")]
    public string Foedselsdato { get; set; }

    [JsonProperty("kjoenn")]
    public string Kjoenn { get; set; }

    [JsonProperty("personnavn")]
    public FregPersonnavnDto Personnavn { get; set; }

    [JsonProperty("statsborgerskap")]
    public List<string> Statsborgerskap { get; set; }
}

[Serializable]
public class FalskIdentitetDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erFalsk")]
    public bool ErFalsk { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("rettIdentitetErUkjent")]
    public bool? RettIdentitetErUkjent { get; set; }

    [JsonProperty("rettIdentitetVedIdentifikasjonsnummer")]
    public string RettIdentitetVedIdentifikasjonsnummer { get; set; }

    [JsonProperty("rettIdentitetVedOpplysninger")]
    public FalskIdentitetRettIdentitetVedOpplysningerDto RettIdentitetVedOpplysninger { get; set; }
}

// ── Part: familierelasjon ─────────────────────────────────────────────────────

[Serializable]
public class FamilierelasjonsDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("minRolleForPerson")]
    public string MinRolleForPerson { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("relatertPerson")]
    public string RelatertPerson { get; set; }

    [JsonProperty("relatertPersonUtenFolkeregisteridentifikator")]
    public FregPersonUtenIdNavnDto RelatertPersonUtenFolkeregisteridentifikator { get; set; }

    [JsonProperty("relatertPersonsRolle")]
    public string RelatertPersonsRolle { get; set; }
}

// ── Part: foedsel ─────────────────────────────────────────────────────────────

[Serializable]
public class FoedselDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("foedekommuneINorge")]
    public string FoedekommuneINorge { get; set; }

    [JsonProperty("foedeland")]
    public string Foedeland { get; set; }

    [JsonProperty("foedested")]
    public string Foedested { get; set; }

    [JsonProperty("foedselsaar")]
    public string Foedselsaar { get; set; }

    [JsonProperty("foedselsdato")]
    public string Foedselsdato { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: foedselINorge ───────────────────────────────────────────────────────

[Serializable]
public class FoedselINorgeDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("foedselsinstitusjonsnavn")]
    public string Foedselsinstitusjonsnavn { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("rekkefoelgenummer")]
    public int? Rekkefoelgenummer { get; set; }
}

// ── Part: foreldreansvar ──────────────────────────────────────────────────────

[Serializable]
public class ForeldreansvarDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("ansvar")]
    public string Ansvar { get; set; }

    [JsonProperty("ansvarlig")]
    public string Ansvarlig { get; set; }

    [JsonProperty("ansvarligOrganisasjon")]
    public string AnsvarligOrganisasjon { get; set; }

    [JsonProperty("ansvarligUtenIdentifikator")]
    public FregPersonUtenIdNavnDto AnsvarligUtenIdentifikator { get; set; }

    [JsonProperty("ansvarssubjekt")]
    public string Ansvarssubjekt { get; set; }

    [JsonProperty("ansvarssubjektUtenIdentifikator")]
    public FregPersonUtenIdNavnDto AnsvarssubjektUtenIdentifikator { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: forholdTilSametingetsValgmanntall ───────────────────────────────────

[Serializable]
public class ForholdTilSametingetsValgmanntallDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("forhold")]
    public string Forhold { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("vedtaksdato")]
    public string Vedtaksdato { get; set; }
}

// ── Part: fratattRettsligHandleevne ───────────────────────────────────────────

[Serializable]
public class FratattRettsligHandleevneDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: identifikasjonsnummer ───────────────────────────────────────────────

[Serializable]
public class IdentifikasjonsnummerDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("foedselsEllerDNummer")]
    public string FoedselsEllerDNummer { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("identifikatortype")]
    public string Identifikatortype { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}

// ── Part: identitetsgrunnlag ──────────────────────────────────────────────────

[Serializable]
public class IdentitetsgrunnlagDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("identitetsgrunnlagstatus")]
    public string Identitetsgrunnlagstatus { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: innflytting ─────────────────────────────────────────────────────────

[Serializable]
public class InnflyttingDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("fraflyttingsland")]
    public string Fraflyttingsland { get; set; }

    [JsonProperty("fraflyttingsstedIUtlandet")]
    public string FraflyttingsstedIUtlandet { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: kjoenn ──────────────────────────────────────────────────────────────

[Serializable]
public class KjoennDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("kjoenn")]
    public string Kjoenn { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }
}

// ── Part: kontaktinformasjonForDoedsbo ────────────────────────────────────────

[Serializable]
public class KontaktinformasjonForDoedsboAdresseDto
{
    [JsonProperty("adresselinje")]
    public List<string> Adresselinje { get; set; }

    [JsonProperty("landkode")]
    public string Landkode { get; set; }

    [JsonProperty("postnummer")]
    public string Postnummer { get; set; }

    [JsonProperty("poststedsnavn")]
    public string Poststedsnavn { get; set; }
}

[Serializable]
public class KontaktinformasjonForDoedsboAdvokatDto
{
    [JsonProperty("organisasjonsnavn")]
    public string Organisasjonsnavn { get; set; }

    [JsonProperty("organisasjonsnummer")]
    public string Organisasjonsnummer { get; set; }

    [JsonProperty("personnavn")]
    public FregPersonnavnDto Personnavn { get; set; }
}

[Serializable]
public class KontaktinformasjonForDoedsboOrganisasjonDto
{
    [JsonProperty("kontaktpersonnavn")]
    public FregPersonnavnDto Kontaktpersonnavn { get; set; }

    [JsonProperty("organisasjonsnavn")]
    public string Organisasjonsnavn { get; set; }

    [JsonProperty("organisasjonsnummer")]
    public string Organisasjonsnummer { get; set; }
}

[Serializable]
public class KontaktinformasjonForDoedsboPersonDto
{
    [JsonProperty("foedselsEllerDNummer")]
    public string FoedselsEllerDNummer { get; set; }

    [JsonProperty("foedselsdato")]
    public string Foedselsdato { get; set; }

    [JsonProperty("personnavn")]
    public FregPersonnavnDto Personnavn { get; set; }
}

[Serializable]
public class KontaktinformasjonForDoedsboDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("adresse")]
    public KontaktinformasjonForDoedsboAdresseDto Adresse { get; set; }

    [JsonProperty("advokat")]
    public KontaktinformasjonForDoedsboAdvokatDto Advokat { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("attestutstedelsesdato")]
    public string Attestutstedelsesdato { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("organisasjon")]
    public KontaktinformasjonForDoedsboOrganisasjonDto Organisasjon { get; set; }

    [JsonProperty("person")]
    public KontaktinformasjonForDoedsboPersonDto Person { get; set; }

    [JsonProperty("skifteform")]
    public string Skifteform { get; set; }
}

// ── Part: legitimasjonsdokument ───────────────────────────────────────────────

[Serializable]
public class DokumentkontrollDto
{
    [JsonProperty("dokumentkontrollstatus")]
    public string Dokumentkontrollstatus { get; set; }

    [JsonProperty("dokumentkontrolltidspunkt")]
    public DateTimeOffset? Dokumentkontrolltidspunkt { get; set; }
}

[Serializable]
public class LegitimasjonsdokumentDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("dokumentkontroll")]
    public DokumentkontrollDto Dokumentkontroll { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldigFra")]
    public string GyldigFra { get; set; }

    [JsonProperty("gyldigTil")]
    public string GyldigTil { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("identifikasjonsdokumentnummer")]
    public string Identifikasjonsdokumentnummer { get; set; }

    [JsonProperty("identifikasjonsdokumenttype")]
    public string Identifikasjonsdokumenttype { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("utstederland")]
    public string Utstederland { get; set; }

    [JsonProperty("utstedernavn")]
    public string Utstedernavn { get; set; }
}

// ── Part: navn ────────────────────────────────────────────────────────────────

[Serializable]
public class NavnDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("etternavn")]
    public string Etternavn { get; set; }

    [JsonProperty("forkortetNavn")]
    public string ForkortetNavn { get; set; }

    [JsonProperty("fornavn")]
    public string Fornavn { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("mellomnavn")]
    public string Mellomnavn { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("originaltNavn")]
    public FregPersonnavnDto OriginaltNavn { get; set; }
}

// ── Part: opphold ─────────────────────────────────────────────────────────────

[Serializable]
public class OppholdDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("oppholdFra")]
    public string OppholdFra { get; set; }

    [JsonProperty("oppholdTil")]
    public string OppholdTil { get; set; }

    [JsonProperty("oppholdstillatelse")]
    public string Oppholdstillatelse { get; set; }
}

// ── Part: oppholdPaaSvalbard ──────────────────────────────────────────────────

[Serializable]
public class OppholdPaaSvalbardDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("antallTidligereOpphold")]
    public int? AntallTidligereOpphold { get; set; }

    [JsonProperty("antattOppholdsvarighet")]
    public string AntattOppholdsvarighet { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("fraflyttingskommunenummer")]
    public string Fraflyttingskommunenummer { get; set; }

    [JsonProperty("fraflyttingsland")]
    public string Fraflyttingsland { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("sluttdato")]
    public string Sluttdato { get; set; }

    [JsonProperty("startdato")]
    public string Startdato { get; set; }
}

// ── Part: oppholdsadresse ─────────────────────────────────────────────────────

[Serializable]
public class OppholdsadresseDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("adresseIdentifikatorFraMatrikkelen")]
    public string AdresseIdentifikatorFraMatrikkelen { get; set; }

    [JsonProperty("adressegradering")]
    public string Adressegradering { get; set; }

    [JsonProperty("adressenErUkjent")]
    public bool? AdressenErUkjent { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("matrikkeladresse")]
    public FregMatrikkeladresseDto Matrikkeladresse { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("oppholdAnnetSted")]
    public string OppholdAnnetSted { get; set; }

    [JsonProperty("oppholdsadressedato")]
    public string Oppholdsadressedato { get; set; }

    [JsonProperty("utenlandskAdresse")]
    public FregUtenlandskAdresseDto UtenlandskAdresse { get; set; }

    [JsonProperty("vegadresse")]
    public FregVegadresseDto Vegadresse { get; set; }
}

// ── Part: postadresse ─────────────────────────────────────────────────────────

[Serializable]
public class PostadresseIFrittFormatDto
{
    [JsonProperty("adresselinje")]
    public List<string> Adresselinje { get; set; }

    [JsonProperty("poststed")]
    public FregPoststedDto Poststed { get; set; }
}

[Serializable]
public class PostboksadresseDto
{
    [JsonProperty("postboks")]
    public string Postboks { get; set; }

    [JsonProperty("postbokseier")]
    public string Postbokseier { get; set; }

    [JsonProperty("poststed")]
    public FregPoststedDto Poststed { get; set; }
}

[Serializable]
public class PostadresseDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("adresseIdentifikatorFraMatrikkelen")]
    public string AdresseIdentifikatorFraMatrikkelen { get; set; }

    [JsonProperty("adressegradering")]
    public string Adressegradering { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("postadresseIFrittFormat")]
    public PostadresseIFrittFormatDto PostadresseIFrittFormat { get; set; }

    [JsonProperty("postboksadresse")]
    public PostboksadresseDto Postboksadresse { get; set; }

    [JsonProperty("vegadresse")]
    public FregVegadresseDto Vegadresse { get; set; }
}

// ── Part: postadresseIUtlandet ────────────────────────────────────────────────

[Serializable]
public class UtenlandskAdresseIFrittFormatDto
{
    [JsonProperty("adresselinje")]
    public List<string> Adresselinje { get; set; }

    [JsonProperty("byEllerStedsnavn")]
    public string ByEllerStedsnavn { get; set; }

    [JsonProperty("landkode")]
    public string Landkode { get; set; }

    [JsonProperty("postkode")]
    public string Postkode { get; set; }
}

[Serializable]
public class PostadresseIUtlandetDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("adressegradering")]
    public string Adressegradering { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("utenlandskAdresse")]
    public FregUtenlandskAdresseDto UtenlandskAdresse { get; set; }

    [JsonProperty("utenlandskAdresseIFrittFormat")]
    public UtenlandskAdresseIFrittFormatDto UtenlandskAdresseIFrittFormat { get; set; }
}

// ── Part: preferertKontaktadresse ─────────────────────────────────────────────

[Serializable]
public class KontaktadresseIFrittFormatDto
{
    [JsonProperty("adresselinje")]
    public List<string> Adresselinje { get; set; }

    [JsonProperty("landkode")]
    public string Landkode { get; set; }
}

[Serializable]
public class PreferertKontaktadresseDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("adressegradering")]
    public string Adressegradering { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("kontaktadresseIFrittFormat")]
    public KontaktadresseIFrittFormatDto KontaktadresseIFrittFormat { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("valg")]
    public string Valg { get; set; }
}

// ── Part: rettsligHandleevne ──────────────────────────────────────────────────

[Serializable]
public class RettsligHandleevneDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("rettsligHandleevneomfang")]
    public string RettsligHandleevneomfang { get; set; }
}

// ── Part: sivilstand ──────────────────────────────────────────────────────────

[Serializable]
public class SivilstandDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("kommune")]
    public string Kommune { get; set; }

    [JsonProperty("myndighet")]
    public string Myndighet { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("relatertVedSivilstand")]
    public string RelatertVedSivilstand { get; set; }

    [JsonProperty("sivilstand")]
    public string Sivilstand { get; set; }

    [JsonProperty("sivilstandsdato")]
    public string Sivilstandsdato { get; set; }

    [JsonProperty("sted")]
    public string Sted { get; set; }

    [JsonProperty("utland")]
    public string Utland { get; set; }
}

// ── Part: statsborgerskap ─────────────────────────────────────────────────────

[Serializable]
public class StatsborgerskapDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("ervervsdato")]
    public string Ervervsdato { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("statsborgerskap")]
    public string Statsborgerskap { get; set; }
}

// ── Part: status ──────────────────────────────────────────────────────────────

[Serializable]
public class StatusDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}

// ── Part: utenlandskPersonidentifikasjon ──────────────────────────────────────

[Serializable]
public class UtenlandskPersonidentifikasjonDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("identifikasjonsnummer")]
    public string Identifikasjonsnummer { get; set; }

    [JsonProperty("identifikasjonsnummertype")]
    public string Identifikasjonsnummertype { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("utstederland")]
    public string Utstederland { get; set; }
}

// ── Part: utflytting ──────────────────────────────────────────────────────────

[Serializable]
public class UtflyttingDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("tilflyttingsland")]
    public string Tilflyttingsland { get; set; }

    [JsonProperty("tilflyttingsstedIUtlandet")]
    public string TilflyttingsstedIUtlandet { get; set; }

    [JsonProperty("utflyttingsdato")]
    public string Utflyttingsdato { get; set; }
}

// ── Part: utlendingsmyndighetenesIdentifikasjonsnummer ────────────────────────

[Serializable]
public class UtlendingsmyndighetenesIdentifikasjonsnummerDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("identifikasjonsnummer")]
    public string Identifikasjonsnummer { get; set; }

    [JsonProperty("identifikasjonsnummertype")]
    public string Identifikasjonsnummertype { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("utstederland")]
    public string Utstederland { get; set; }
}

// ── Part: vergemaalEllerFremtidsfullmakt ──────────────────────────────────────

[Serializable]
public class VergeTjenesteDto
{
    [JsonProperty("vergeTjenesteoppgave")]
    public string VergeTjenesteoppgave { get; set; }

    [JsonProperty("vergeTjenestevirksomhet")]
    public string VergeTjenestevirksomhet { get; set; }
}

[Serializable]
public class VergeDto
{
    [JsonProperty("foedselsEllerDNummer")]
    public string FoedselsEllerDNummer { get; set; }

    [JsonProperty("navn")]
    public FregPersonnavnDto Navn { get; set; }

    [JsonProperty("navnFoedselsdato")]
    public FregPersonUtenIdPersonnavnDto NavnFoedselsdato { get; set; }

    [JsonProperty("omfang")]
    public string Omfang { get; set; }

    [JsonProperty("omfangetErInnenPersonligOmraade")]
    public bool? OmfangetErInnenPersonligOmraade { get; set; }

    [JsonProperty("tjenesteomraade")]
    public List<VergeTjenesteDto> Tjenesteomraade { get; set; }
}

[Serializable]
public class VergemaalEllerFremtidsfullmaktDto
{
    [JsonProperty("aarsak")]
    public string Aarsak { get; set; }

    [JsonProperty("ajourholdstidspunkt")]
    public DateTimeOffset? Ajourholdstidspunkt { get; set; }

    [JsonProperty("embete")]
    public string Embete { get; set; }

    [JsonProperty("erGjeldende")]
    public bool ErGjeldende { get; set; }

    [JsonProperty("gyldighetstidspunkt")]
    public DateTimeOffset? Gyldighetstidspunkt { get; set; }

    [JsonProperty("kilde")]
    public string Kilde { get; set; }

    [JsonProperty("opphoerstidspunkt")]
    public DateTimeOffset? Opphoerstidspunkt { get; set; }

    [JsonProperty("verge")]
    public VergeDto Verge { get; set; }

    [JsonProperty("vergemaaltype")]
    public string Vergemaaltype { get; set; }
}
