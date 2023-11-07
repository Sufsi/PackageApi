using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Database
{
    public interface IDatabase
    {
        bool AddPackage(Package package);
        Package GetPackageDimensions(string kolliId);
        IEnumerable<Package> GetAllPackages();
    }
}
