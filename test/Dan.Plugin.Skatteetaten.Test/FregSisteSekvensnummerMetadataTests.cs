using System.Linq;
using AwesomeAssertions;
using Dan.Common.Enums;
using Dan.Common.Models;
using Dan.Plugin.DATASOURCENAME;
using Xunit;

namespace Dan.Plugin.Skatteetaten.Test
{
    public class FregSisteSekvensnummerMetadataTests
    {
        private static EvidenceCode GetFregSisteSekvensnummer()
            => new Metadata().GetEvidenceCodes().Single(ec => ec.EvidenceCodeName == "FregSisteSekvensnummer");

        [Fact]
        public void Is_declared_for_oed_and_altinn_studio_apps()
        {
            var ec = GetFregSisteSekvensnummer();

            ec.BelongsToServiceContexts.Should().BeEquivalentTo("OED", "Altinn Studio-apps");
            ec.RequiredScopes.Should().Be("folkeregister:deling/offentligmedhjemmel");
        }

        [Fact]
        public void Requires_own_token()
        {
            var ec = GetFregSisteSekvensnummer();

            ec.AuthorizationRequirements.Should().ContainSingle(r => r is ProvideOwnTokenRequirement);
        }

        [Fact]
        public void Allows_any_subject()
        {
            var ec = GetFregSisteSekvensnummer();

            var subjectRequirement = ec.AuthorizationRequirements
                .OfType<CustomSubjectRequirement>()
                .Should().ContainSingle().Subject;
            subjectRequirement.SubjectRegex.Should().Be(".*");
        }

        [Fact]
        public void Takes_no_parameters()
        {
            var ec = GetFregSisteSekvensnummer();

            ec.Parameters.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Publishes_a_single_numeric_default_value()
        {
            var ec = GetFregSisteSekvensnummer();

            var value = ec.Values.Should().ContainSingle().Subject;
            value.EvidenceValueName.Should().Be("sekvensnummer");
            value.ValueType.Should().Be(EvidenceValueType.Number);
        }
    }
}
