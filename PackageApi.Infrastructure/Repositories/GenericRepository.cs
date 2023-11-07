using PackageApi.Infrastructure.Database;
using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    public GenericRepository(IDatabase database)
    {
        Database = database;
    }

    public IDatabase Database { get; }

    public Task Create(T entity)
    {
        var result = Database.AddPackage(entity as Package);
        return Task.FromResult(result);
    }

    public Task<T> Get(string id)
    {
        var result = Database.GetPackageDimensions(id);

        return Task.FromResult(result as T);
    }

    public Task<IEnumerable<T>> GetAll()
    {
        var result = Database.GetAllPackages();


        return Task.FromResult((IEnumerable<T>)result);
    }
}
