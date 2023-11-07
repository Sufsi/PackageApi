using Microsoft.Extensions.DependencyInjection;
using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider ServiceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IGenericRepository<T> GetRepository<T>() where T : Package
        {
            return ServiceProvider.GetRequiredService<IGenericRepository<T>>();
        }
    }
}
