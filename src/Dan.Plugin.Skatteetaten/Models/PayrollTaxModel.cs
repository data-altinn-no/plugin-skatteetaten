using System;
using System.Collections.Generic;
using System.Text;

namespace Dan.Plugin.Skatteetaten.Models.Arbeidsgiveravgift
{
    public class Arbeidsgiveravgift
    {
        public string termin { get; set; }
        public string aar { get; set; }
        public int sumavgiftsgrunnlagBeloep { get; set; }
    }

    public class PayrollTaxModel
    {
        public DateTime levert { get; set; }
        public string forespurteOrganisasjon { get; set; }
        public List<Arbeidsgiveravgift> arbeidsgiveravgifter { get; set; }
    }


}
