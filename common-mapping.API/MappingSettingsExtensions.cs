namespace common_mapping.API
{
    public static class MappingSettingsExtensions
    {
        public static void AddMappingSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("MappingSettings").Get<Settings.Setup>();
            
            services.Configure<Settings.Setup>(cnf =>
            {
                cnf.ConnectionType = config.ConnectionType;
                cnf.MSSQL = config.MSSQL;
                cnf.API = config.API;
                cnf.SQLite = config.SQLite;
            });
        }
    }
}
