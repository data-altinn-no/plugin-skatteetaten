using System;

namespace Dan.Plugin.Skatteetaten.Models.Arrears
{
    public class ArrearsModel
    {
        public DateTime levert { get; set; }
        public string forespurteOrganisasjon { get; set; }
        public Restanser restanser { get; set; }
    }

    public class RestanserKategori
    {
        public decimal forfaltOgUbetalt { get; set; }
        public decimal forfaltOgUbetaltRenter { get; set; }
        public decimal forfaltOgUbetaltKrav { get; set; }
    }

    public class Restanser
    {
        public RestanserKategori arbeidsgiveravgift { get; set; }
        public RestanserKategori forskuddstrekk { get; set; }
        public RestanserKategori forskuddsskatt { get; set; }
        public RestanserKategori restskatt { get; set; }
        public RestanserKategori gebyr { get; set; }
        public RestanserKategori merverdiavgift { get; set; }
    }
}
