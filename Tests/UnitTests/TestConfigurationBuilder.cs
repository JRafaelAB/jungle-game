using Microsoft.Extensions.Configuration;

namespace UnitTests;

public static class TestConfigurationBuilder
{
    public static IConfiguration BuildTestConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true);
        return builder.Build();
    }
}
