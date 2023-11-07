using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    public Task Create(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }
}
