namespace Mango.Services.ProductApi.Config;

public static class AppConfig
{
    private static IConfiguration currentConfig;

    public static void SetConfig(IConfiguration configuration)
    {
        currentConfig = configuration;
    }

    public static string GetConfiguration(string configKey)
    {
        try
        {
            string connectionString = currentConfig.GetConnectionString(configKey);
            return connectionString;
        }
        catch (Exception e)
        {
            throw e.InnerException!;
        }

        return "";
    }
    
}