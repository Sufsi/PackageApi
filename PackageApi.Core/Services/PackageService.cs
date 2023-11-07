using PackageApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageApi.Core.Services
{
    public class PackageService
    {
        public PackageService()
        {
            
        }

        public Task<List<Package>> GetPackages()
        {
            var result = new List<Package>();

            return Task.FromResult(result);
        }
    }
}
