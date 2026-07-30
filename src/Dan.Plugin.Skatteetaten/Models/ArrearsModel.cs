using System;
using System.Collections.Generic;
using System.Text;

namespace Dan.Plugin.Skatteetaten.Models.Arrears
{
    class ArrearsModel
    {
        public DateTime levert { get; set; }
        public int forespurteOrganisasjon { get; set; }
        public Restanser restanser { get; set; }
    }

    public class Arbeidsgiveravgift
    {
        public int forfaltOgUbetalt { get; set; }
    }

    public class Forskuddstrekk
    {
        public int forfaltOgUbetalt { get; set; }
    }

    public class Forskuddsskatt
    {
        public int forfaltOgUbetalt { get; set; }
    }

    public class Restskatt
    {
        public int forfaltOgUbetalt { get; set; }
    }

    public class Gebyr
    {
        public int forfaltOgUbetalt { get; set; }
    }

    public class Merverdiavgift
    {
        public int forfaltOgUbetalt { get; set; }
    }

    public class Restanser
    {
        public Arbeidsgiveravgift arbeidsgiveravgift { get; set; }
        public Forskuddstrekk forskuddstrekk { get; set; }
        public Forskuddsskatt forskuddsskatt { get; set; }
        public Restskatt restskatt { get; set; }
        public Gebyr gebyr { get; set; }
        public Merverdiavgift merverdiavgift { get; set; }
    }
}

  
