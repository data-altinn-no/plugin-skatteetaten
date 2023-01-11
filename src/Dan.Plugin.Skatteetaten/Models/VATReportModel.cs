using System;
using System.Collections.Generic;
using System.Text;

namespace Dan.Plugin.Skatteetaten.Models
{   
    public class FoersteTermin
    {
        public string termin { get; set; }
        public string aar { get; set; }
    }

    public class SisteTermin
    {
        public string termin { get; set; }
        public string aar { get; set; }
    }

    public class Skattemeldingsplikt
    {
        public string termintype { get; set; }
        public FoersteTermin foersteTermin { get; set; }
        public SisteTermin sisteTermin { get; set; }
    }

    public class AnsvarligForMvaMelding
    {
        public int organisasjonsnummer { get; set; }
        public string oragnisasjonsnavn { get; set; }
    }

    public class GjelderTermin
    {
        public string termin { get; set; }
        public string aar { get; set; }
    }

    public class MvaAvgift
    {
        public int innlandOmsetningUttakHoeySats { get; set; }
        public int innlandOmsetningUttakMiddelsSats { get; set; }
        public int innlandOmsetningUttakLavSats { get; set; }
        public int fradragInnlandInngaaendeHoeySats { get; set; }
        public int fradragInnlandInngaaendeMiddelsSats { get; set; }
        public int fradragInnlandInngaaendeLavSats { get; set; }
    }

    public class MvaGrunnlag
    {
        public int innlandOmsetningUttakHoeySats { get; set; }
        public int innlandOmsetningUttakMiddelsSats { get; set; }
        public int innlandOmsetningUttakLavSats { get; set; }
    }

    public class SamletFastsattOgReskontrofoertForTermin
    {
        public GjelderTermin gjelderTermin { get; set; }
        public string fastsettingsperiodeStatus { get; set; }
        public bool? erMyndighetsfastsatt { get; set; }
        public string grunnMyndighetsfastsatt { get; set; }
        public MvaAvgift mvaAvgift { get; set; }
        public MvaGrunnlag mvaGrunnlag { get; set; }
    }

    public class MvaAlminneligNaering
    {
        public Skattemeldingsplikt skattemeldingsplikt { get; set; }
        public AnsvarligForMvaMelding ansvarligForMvaMelding { get; set; }
        public List<SamletFastsattOgReskontrofoertForTermin> samletFastsattOgReskontrofoertForTermin { get; set; }
    }

    public class VATReportModel
    {
        public DateTime levert { get; set; }
        public string forespurteOrganisasjon { get; set; }
        public MvaAlminneligNaering mvaAlminneligNaering { get; set; }
    }


}
