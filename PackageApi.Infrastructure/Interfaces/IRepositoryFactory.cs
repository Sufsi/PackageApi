using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageApi.Infrastructure
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> GetRepository<T>() where T : Package;
    }
}
