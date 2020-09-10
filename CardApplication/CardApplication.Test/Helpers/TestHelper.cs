using Microsoft.Extensions.Configuration;

namespace CardApplication.Test.Helpers
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

        public static string GetConnectionString(string outputPath)
        {
            var config = GetIConfigurationRoot(outputPath);
            return config["ConnectionStrings:DefaultConnection"];
        }
    }
}
