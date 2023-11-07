using PackageApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageApi.Infrastructure.Database;

public class PackageDatabase : IDatabase
{
    private static List<Package> packages = new List<Package>() { new Package("9999Test", 23, new Dimensions("32", "54", "64")) };

    public bool AddPackage(Package package)
    {
        packages.Add(package);
        return true;
    }

    public Package GetPackageDimensions(string kolliId)
    {
        return packages.Where(pk => pk.KolliId == kolliId).First();
    }

    public IEnumerable<Package> GetAllPackages()
    {
        return packages;
    }
}
