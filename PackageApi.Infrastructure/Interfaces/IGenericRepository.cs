using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Interfaces;

public interface IGenericRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(string id);
    Task<bool> Create(T entity);
}
