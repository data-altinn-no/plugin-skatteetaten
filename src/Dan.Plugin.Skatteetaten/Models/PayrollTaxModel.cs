using System;
using System.Collections.Generic;

namespace Dan.Plugin.Skatteetaten.Models.Arbeidsgiveravgift
{
    public class ArbeidsgiveravgiftEntry
    {
        public string termin { get; set; }
        public string aar { get; set; }
        public long sumavgiftsgrunnlagBeloep { get; set; }
    }

    public class ArbeidsgiveravgiftWrapper
    {
        public ArbeidsgiveravgiftEntry arbeidsgiveravgift { get; set; }
    }

    public class PayrollTaxModel
    {
        public DateTime levert { get; set; }
        public string forespurteOrganisasjon { get; set; }
        public List<ArbeidsgiveravgiftWrapper> arbeidsgiveravgifter { get; set; }
    }
}
