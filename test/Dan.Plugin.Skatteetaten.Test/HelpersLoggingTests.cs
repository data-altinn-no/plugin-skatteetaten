using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Dan.Common.Exceptions;
using Dan.Common.Models;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Utilities;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Dan.Plugin.Skatteetaten.Test
{
    public class HelpersLoggingTests
    {
        // Synthetic Tenor test identifier, not a real national identity number.
        private const string Ssn = "06815298657";
        private const string Jwt = "consent-jwt-secret-value";
        private const string MpToken = "maskinporten-token-secret-value";

        private static EvidenceHarvesterRequest CreateRequest() => new()
        {
            EvidenceCodeName = "FregPerson",
            OrganizationNumber = Ssn,
            SubjectParty = new Party { NorwegianSocialSecurityNumber = Ssn },
            JWT = Jwt,
            MPToken = MpToken
        };

        private static HttpClient CreateClient(HttpStatusCode status, string body)
            => new(new StubHandler(status, body)) { BaseAddress = new Uri("https://api.skatteetaten.no/") };

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Request_log_masks_subject_and_never_contains_tokens(bool isDev)
        {
            var logger = new CapturingLogger();
            var settings = new ApplicationSettings { IsDevEnvironment = isDev };

            await Helpers.HarvestFromSke(CreateRequest(), logger, CreateClient(HttpStatusCode.OK, "{\"a\":1}"), HttpMethod.Get, "personer/x", settings);

            var line = logger.Messages.Should().ContainSingle().Subject;
            line.Should().Contain("068152*****");
            line.Should().NotContain(Ssn);
            line.Should().NotContain(Jwt);
            line.Should().NotContain(MpToken);
        }

        [Fact]
        public async Task Upstream_error_body_is_masked_in_the_log()
        {
            var logger = new CapturingLogger();
            var body = $"{{\"kode\":\"FREG-004\",\"melding\":\"Fant ikke person med identifikator {Ssn}\"}}";

            var act = () => Helpers.HarvestFromSke(CreateRequest(), logger, CreateClient(HttpStatusCode.NotFound, body), HttpMethod.Get, "personer/x");

            await act.Should().ThrowAsync<EvidenceSourceTransientException>();
            var warning = logger.Messages.Last();
            warning.Should().Contain("404");
            warning.Should().Contain("Fant ikke person med identifikator 068152*****");
            warning.Should().NotContain(Ssn);
        }

        [Fact]
        public void SanitizeForLog_truncates_long_upstream_text()
        {
            var sanitized = Helpers.SanitizeForLog(new string('x', 2000));

            sanitized.Should().HaveLength(500 + "...(truncated)".Length);
            sanitized.Should().EndWith("...(truncated)");
        }

        private sealed class StubHandler(HttpStatusCode status, string body) : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                => Task.FromResult(new HttpResponseMessage(status) { Content = new StringContent(body) });
        }

        private sealed class CapturingLogger : ILogger
        {
            public List<string> Messages { get; } = new();

            public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
                => Messages.Add(formatter(state, exception));
        }
    }
}
