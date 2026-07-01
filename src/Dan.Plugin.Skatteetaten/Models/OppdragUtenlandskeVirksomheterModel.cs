namespace Dan.Plugin.Skatteetaten.Models.OppdragUtenlandskeVirksomheter
{
    public class OppdragAntall
    {
        public int antallAktiveOppdragSomArbeidsgiver { get; set; }
        public int antallAktiveArbeidstakere { get; set; }
        public int antallRegistrerteOppdragSomOppdragsgiver { get; set; }
    }

    public class OppdragUtenlandskeVirksomheterModel
    {
        public string forespurteOrganisasjon { get; set; }
        public OppdragAntall oppdrag { get; set; }
    }
}
