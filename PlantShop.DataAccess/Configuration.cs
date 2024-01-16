using Microsoft.Extensions.Configuration;

namespace PlantShop.DataAccess
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PlantShop.WebUI"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("PlantShopDbConnection");
            }
        }
    }
}
