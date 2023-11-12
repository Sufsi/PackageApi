using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PackageApi.Infrastructure;
using PackageApi.Models;

namespace PackageApi.Facades;
public interface IPackageFacade
{
    ActionResult<bool> CreatePackage(Package package);
    ActionResult<Dimensions> GetPackageDimensions(string kolliId);
    ActionResult<IEnumerable<Package>> GetPackages();
}
public class PackageFacade : IPackageFacade
{
    private readonly IRepositoryFactory repositoryFactory;

    public PackageFacade(IRepositoryFactory repositoryFactory)
    {
        this.repositoryFactory = repositoryFactory;
    }

    public ActionResult<IEnumerable<Package>> GetPackages()
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.GetAll().Result;

        var packages = result.Select(item => ConvertToPackage(item));
        return new OkObjectResult(packages);
    }

    public ActionResult<Dimensions> GetPackageDimensions(string kolliId)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.Get(kolliId).Result;

        if (result == null)
        {
            return new NotFoundResult();
        }

        var package = ConvertToPackage(result);
        return new OkObjectResult(package.Dimensions);
    }

    public ActionResult<bool> CreatePackage(Package package)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var infrastructurePackage = ConvertToInfrastructurePackage(package);
        var result = repo.Create(infrastructurePackage);

        return true;
    }

    private static Package ConvertToPackage(Infrastructure.Models.Package infrastructurePackage)
    {
        return new Package(
            infrastructurePackage.KolliId,
            new Dimensions(
                infrastructurePackage.Dimensions.Weight,
                infrastructurePackage.Dimensions.Length,
                infrastructurePackage.Dimensions.Height,
                infrastructurePackage.Dimensions.Width
            )
        );
    }

    private static Infrastructure.Models.Package ConvertToInfrastructurePackage(Package package)
    {
        return new Infrastructure.Models.Package(
            package.KolliId,
            new Infrastructure.Models.Dimensions(
                package.Dimensions.Weight,
                package.Dimensions.Length,
                package.Dimensions.Height,
                package.Dimensions.Width
            )
        );
    }
}
