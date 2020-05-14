using GraphBfs.RepositoriesContracts;
using GraphBfs.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyExtentions
    {
        public static void AddReposiotories(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ILogisticCenterRepository, LogisticCenterRepository>();
            services.AddScoped<IPathRepository, PathRepository>();
        }
    }
}
