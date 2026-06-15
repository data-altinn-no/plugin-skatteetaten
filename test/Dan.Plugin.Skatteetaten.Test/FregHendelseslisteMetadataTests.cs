using System.Linq;
using AwesomeAssertions;
using Dan.Common.Models;
using Dan.Plugin.DATASOURCENAME;
using Xunit;

namespace Dan.Plugin.Skatteetaten.Test
{
    public class FregHendelseslisteMetadataTests
    {
        private static EvidenceCode GetFregHendelsesliste()
            => new Metadata().GetEvidenceCodes().Single(ec => ec.EvidenceCodeName == "FregHendelsesliste");

        [Fact]
        public void Is_declared_for_oed_and_altinn_studio_apps()
        {
            var ec = GetFregHendelsesliste();

            ec.BelongsToServiceContexts.Should().BeEquivalentTo("OED", "Altinn Studio-apps");
            ec.RequiredScopes.Should().Be("folkeregister:deling/offentligmedhjemmel");
        }

        [Fact]
        public void Requires_own_token()
        {
            var ec = GetFregHendelsesliste();

            ec.AuthorizationRequirements.Should().ContainSingle(r => r is ProvideOwnTokenRequirement);
        }

        [Fact]
        public void Allows_any_subject()
        {
            var ec = GetFregHendelsesliste();

            var subjectRequirement = ec.AuthorizationRequirements
                .OfType<CustomSubjectRequirement>()
                .Should().ContainSingle().Subject;
            subjectRequirement.SubjectRegex.Should().Be(".*");
        }

        [Fact]
        public void Has_required_sekvensnummer_parameter()
        {
            var ec = GetFregHendelsesliste();

            var param = ec.Parameters.Should().ContainSingle().Subject;
            param.EvidenceParamName.Should().Be("sekvensnummer");
            param.Required.Should().BeTrue();
        }

        [Fact]
        public void Publishes_a_json_schema_for_the_default_value()
        {
            var ec = GetFregHendelsesliste();

            var value = ec.Values.Should().ContainSingle().Subject;
            value.EvidenceValueName.Should().Be("default");
            value.JsonSchemaDefintion.Should().NotBeNullOrWhiteSpace();
        }
    }
}
