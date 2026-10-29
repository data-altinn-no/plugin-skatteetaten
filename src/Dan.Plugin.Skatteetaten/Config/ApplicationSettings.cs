using System;

namespace Dan.Plugin.Skatteetaten.Config
{
    public class ApplicationSettings
    {
        public int Breaker_FailureCountThreshold { get; set; }

        public TimeSpan Breaker_HalfOpenWaitTime { get; set; }

        public int Breaker_RetryCount { get; set; }

        public TimeSpan Breaker_RetryWaitTime { get; set; }

        public int Breaker_SuccessCountThreshold { get; set; }

        public bool IsDevEnvironment { get; set; }

        public bool IsUnitTest { get; set; }

        public string ServiceEndpoint { get; set; }

        public string SummertSkattegrunnlagEndpoint { get; set; }
        public string SisteTilgjengeligeSkatteoppgjoerEndpoint { get; set; }
        public string RestanserEndpoint { get; set; }
        public string OppdragUtenlandskeVirksomheterEndpoint { get; set; }
        public string ArbeidsgiveravgiftEndpoint { get; set; }
        public string MvaMeldingsOpplysningEndpoint { get; set; }

        public string FregEnvironment { get; set; }
    }
}
