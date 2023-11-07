using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    public Task Create(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task GetAll()
    {
        throw new NotImplementedException();
    }
}
