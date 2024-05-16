using Microsoft.Extensions.Configuration;

namespace Domain.Utils
{
    public static class Configuration
    {
        private static IConfiguration? ConfigurationVariables { get; set; }

        public static void SetConfiguration(IConfiguration configuration)
        {
            ConfigurationVariables = configuration;
        }

        public static T? GetConfigurationValue<T>(string name)
        {
            ConfigurationVariables.ValidateNullArgument(nameof(ConfigurationVariables));
            return ConfigurationVariables!.GetValue<T>(name);
        }
    }
}
