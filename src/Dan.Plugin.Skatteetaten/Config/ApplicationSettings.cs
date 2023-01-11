using System;
using System.Security.Cryptography.X509Certificates;

namespace Dan.Plugin.Skatteetaten.Config
{
    public class ApplicationSettings
    {
        public static ApplicationSettings ApplicationConfig;
        private static X509Certificate2 _altinnCertificate;
        public ApplicationSettings()
        {
            ApplicationConfig = this;
        }

        /// <summary>
        /// Redis  Connection String
        /// </summary>
        public string RedisConnectionString { get; set; }

        /// <summary>
        /// Redis Database
        /// </summary>
        public int RedisDatabase { get; set; }

        public int Breaker_FailureCountThreshold { get; set; }

        public TimeSpan Breaker_HalfOpenWaitTime { get; set; }

        public int Breaker_RetryCount { get; set; }

        public TimeSpan Breaker_RetryWaitTime { get; set; }

        public int Breaker_SuccessCountThreshold { get; set; }

        public string RedisCache { get; set; }

        public bool IsDevEnvironment { get; set; }

        public bool IsUnitTest { get; set; }

        public string ServiceEndpoint { get; set; }

        public string KeyVaultName { get; set; }
        public string KeyVaultSslCertificate { get; set; }

        public string FregEnvironment { get; set; }

        public X509Certificate2 AltinnCertificate
        {
            get
            {
                return _altinnCertificate ?? new PluginKeyVault(ApplicationConfig.KeyVaultName).GetCertificate(ApplicationConfig.KeyVaultSslCertificate).Result;
            }
            set
            {
                _altinnCertificate = value;
            }
        }
    }
}
