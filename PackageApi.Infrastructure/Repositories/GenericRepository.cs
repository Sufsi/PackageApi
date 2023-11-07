using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageApi.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Package
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
}
