using Microsoft.Extensions.DependencyInjection;
using PowerChallengeBusiness.Operations;


namespace PowerChallengeBusiness
{
    public static class OperationsManager
    {
        public static IServiceCollection AddOperationExtensions(this IServiceCollection services)
        {
            services.AddTransient<PowerOperation>();

            services.AddScoped<FuelCostOperation>();

            return services;
        }
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services)
        {
            return services;
        }
    }
}
