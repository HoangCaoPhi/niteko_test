using BL.Implement;
using BL.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BL
{
    /// <summary>
    /// Extension Methods thêm add transient cơ chế DI
    /// </summary>
    public static class StartUpExtension
    {
        /// <summary>
        /// Thêm service cơ chế DI
        /// HCPHI 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void DependencyInjectionBL(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDatabaseServiceBL, DatabaseServiceBL>();
            serviceCollection.AddSingleton<IConfigService, ConfigService>();
            serviceCollection.AddTransient<ICategoryBL, CategoryBL>();
            serviceCollection.AddTransient<ICustomerBL, CustomerBL>();
            serviceCollection.AddTransient<IProductBL, ProductBL>();
            serviceCollection.AddTransient<IOrderBL, OrderBL>();
            serviceCollection.AddTransient<IAuthenticate, AuthenticateBL>();
            serviceCollection.AddTransient<ISessionBL, SessionBL>();
            serviceCollection.AddTransient<ILogService, LogService>();
        }
    }
}
