using Boyles.PowerShell.Common.Configuration;
using FluentAssertions;

namespace Boyles.PowerShell.Common.Tests
{
    public class ConfigHelperTests
    {
        private record MockConfiguration(string BaseUrl);

        private const string ConfigPath = "C:\\Temp\\mockConfig.dat";
        private const string BaseUrl = "https://my.website.com";

        private MockConfiguration ConfigObject { get; } = new(BaseUrl);

        [Fact]
        public void SavesConfigurationObject()
        {
            var result = ConfigHelper.SaveConfiguration(ConfigObject, ConfigPath);
            result.Should().BeTrue("We specified a valid configuration object and file path");
        }

        [Fact]
        public void ReadsConfigurationAsObject()
        {
            var result = ConfigHelper.GetConfiguration<MockConfiguration>(ConfigPath);
            result.Should().NotBeNull();

            result.BaseUrl.Should().NotBeNullOrWhiteSpace();
            result.BaseUrl.Should().Be(BaseUrl);
        }
    }
}