using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure;

public interface IRepositoryFactory
{
    IGenericRepository<T> GetRepository<T>() where T : Entity;
}
