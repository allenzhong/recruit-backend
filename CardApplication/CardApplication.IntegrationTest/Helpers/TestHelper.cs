using Microsoft.Extensions.Configuration;

namespace CardApplication.IntegrationTest.Helpers
{
    public class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true);
            
            return configuration.Build();
        }

        public static AuthConfigOptions GetAuthConfig(string outputPath)
        {
            var config = GetIConfigurationRoot(outputPath);
            var authConfig = config.GetSection("Auth0").Get<AuthConfigOptions>();
    
            return authConfig;
        }
        
        public static ServerOptions GetServerOptions(string outputPath)
        {
            var config = GetIConfigurationRoot(outputPath);
            var serverOptions = config.GetSection("Server").Get<ServerOptions>();
    
            return serverOptions;
        }
    }
}