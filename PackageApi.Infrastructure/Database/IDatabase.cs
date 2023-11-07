using PackageApi.Infrastructure.Models;

namespace PackageApi.Infrastructure.Database;

public interface IDatabase
{
    bool AddPackage(Package package);
    Package GetPackage(string kolliId);
    IEnumerable<Package> GetAllPackages();
}
