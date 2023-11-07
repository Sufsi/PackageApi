using PackageApi.Infrastructure.Models;
using System;
namespace PackageApi.Infrastructure.Database;

public class PackageDatabase : IDatabase
{
    private static List<Package> packages = new List<Package>() { new Package("9999Test", 23, new Dimensions("32", "54", "64")) };

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
