using BL.Interface;
using Microsoft.Extensions.Configuration;

namespace BL
{
    class ConfigService : IConfigService
    {
        private readonly IConfigurationSection _appSettings;
        private readonly IConfigurationSection _connectionString;
        private readonly IConfigurationSection _jwt;

        public ConfigService(IConfiguration configuration)
        {
            _appSettings = configuration.GetSection("AppSettings");
            _connectionString = configuration.GetSection("ConnectionStrings");
            _jwt = configuration.GetSection("JWT");
        }

        /// <summary>
        /// Lấy dữ liệu ở app setting theo key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// HCPHI 
        public string GetAppSetting(string key)
        {
            return _appSettings[key];
        }

        public string GetJWT(string key)
        {
            return _jwt[key];
        }

        /// <summary>
        /// Lấy connection string theo key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// HCPHI
        public string GetConnectionString(string key)
        {
            return _connectionString[key];
        }
    }
}