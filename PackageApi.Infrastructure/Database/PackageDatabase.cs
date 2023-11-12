using PackageApi.Infrastructure.Models;
using System;
namespace PackageApi.Infrastructure.Database;

public class PackageDatabase : IDatabase
{
    private static List<Package> packages = new List<Package>() { new Package("999123450000000000", new Dimensions(200, 32, 54, 64, true)), new Package("999543210000000000", new Dimensions(300, 85, 50, 30, false)) };

    public bool AddPackage(Package package)
    {
        packages.Add(package);
        return true;
    }

    public Package GetPackage(string kolliId)
    {
        return packages.FirstOrDefault(pk => pk.KolliId == kolliId);
    }

    public IEnumerable<Package> GetAllPackages()
    {
        return packages;
    }
}
