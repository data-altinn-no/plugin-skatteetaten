using System.Diagnostics;
using AwesomeAssertions;
using Dan.Plugin.Skatteetaten.Utilities;
using Xunit;

namespace Dan.Plugin.Skatteetaten.Test
{
    public class SsnRedactingProcessorTests
    {
        // Subject numbers below are synthetic Tenor test identifiers, not real national identity numbers.
        private static readonly ActivitySource Source = new("Dan.Plugin.Skatteetaten.Test");

        static SsnRedactingProcessorTests()
        {
            ActivitySource.AddActivityListener(new ActivityListener
            {
                ShouldListenTo = _ => true,
                Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData
            });
        }

        [Theory]
        [InlineData("https://api.skatteetaten.no/folkeregisteret/api/v1/personer/06815298657?part=person-basis", "https://api.skatteetaten.no/folkeregisteret/api/v1/personer/068152*****?part=person-basis")]
        [InlineData("https://api.skatteetaten.no/summertskattegrunnlag/v2/oppgjoer/pkg/2024/27907898224", "https://api.skatteetaten.no/summertskattegrunnlag/v2/oppgjoer/pkg/2024/279078*****")]
        [InlineData("https://api.skatteetaten.no/folkeregisteret/api/v1/personer/66829700248?part=person-basis", "https://api.skatteetaten.no/folkeregisteret/api/v1/personer/668297*****?part=person-basis")]
        [InlineData("https://api.skatteetaten.no/restanser/v2/ebevis/991825827", "https://api.skatteetaten.no/restanser/v2/ebevis/991825827")]
        [InlineData("https://api.skatteetaten.no/feed/?seq=123456789012", "https://api.skatteetaten.no/feed/?seq=123456789012")]
        public void Mask_keeps_first_six_digits_and_leaves_other_numbers_alone(string input, string expected)
        {
            SsnRedactingProcessor.Mask(input).Should().Be(expected);
        }

        [Fact]
        public void OnEnd_masks_url_tags_and_display_name()
        {
            using var activity = Source.StartActivity("GET /personer/06815298657");
            activity.Should().NotBeNull();
            activity.SetTag("url.full", "https://api.skatteetaten.no/folkeregisteret/api/v1/personer/06815298657?part=person-basis");
            activity.SetTag("url.path", "/folkeregisteret/api/v1/personer/06815298657");
            activity.SetTag("server.address", "api.skatteetaten.no");

            new SsnRedactingProcessor().OnEnd(activity);

            activity.GetTagItem("url.full").Should().Be("https://api.skatteetaten.no/folkeregisteret/api/v1/personer/068152*****?part=person-basis");
            activity.GetTagItem("url.path").Should().Be("/folkeregisteret/api/v1/personer/068152*****");
            activity.GetTagItem("server.address").Should().Be("api.skatteetaten.no");
            activity.DisplayName.Should().Be("GET /personer/068152*****");
        }
    }
}
