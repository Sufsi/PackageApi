using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Interfaces;

public interface IGenericRepository<T> where T : Entity
{
    Task GetAll();
    Task Get(string id);
    Task Create(T entity);
}
