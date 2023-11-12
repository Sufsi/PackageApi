using FluentValidation;
using PackageApi.Infrastructure;
using PackageApi.Models;

namespace PackageApi.Facades;
public interface IPackageFacade
{
    bool CreatePackage(Package package);
    Package GetPackage(string kolliId);
    IEnumerable<Package> GetPackages();
}
public class PackageFacade : IPackageFacade
{
    private readonly ILogger<PackageFacade> logger;
    private readonly IRepositoryFactory repositoryFactory;
    private readonly IValidator<Package> packageValidator;

    public PackageFacade(ILogger<PackageFacade> logger, IRepositoryFactory repositoryFactory, IValidator<Package> packageValidator)
    {
        this.logger = logger;
        this.repositoryFactory = repositoryFactory;
        this.packageValidator = packageValidator;
    }

    public IEnumerable<Package> GetPackages()
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.GetAll().Result;

        var packages = result.Select(ConvertToPackage);
        return packages;
    }

    public Package GetPackage(string kolliId)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.Get(kolliId).Result;

        var package = ConvertToPackage(result);
        return package;
    }

    public bool CreatePackage(Package package)
    {
        var validationResult = packageValidator.Validate(package);
        logger.LogInformation(validationResult.IsValid ? $"{package.KolliId} valid package" : $"{package.KolliId} contains properties outside of limitations");

        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var infrastructurePackage = ConvertToInfrastructurePackage(package, validationResult.IsValid);
        var result = repo.Create(infrastructurePackage);

        return result != null;
    }

    private static Package ConvertToPackage(Infrastructure.Models.Package infrastructurePackage)
    {
        return new Package(
            infrastructurePackage.KolliId,
            new Dimensions(
                infrastructurePackage.Dimensions.Weight,
                infrastructurePackage.Dimensions.Length,
                infrastructurePackage.Dimensions.Height,
                infrastructurePackage.Dimensions.Width,
                infrastructurePackage.Dimensions.IsValid
            )
        );
    }

    private static Infrastructure.Models.Package ConvertToInfrastructurePackage(Package package, bool isValid)
    {
        return new Infrastructure.Models.Package(
            package.KolliId,
            new Infrastructure.Models.Dimensions(
                package.Dimensions.Weight,
                package.Dimensions.Length,
                package.Dimensions.Height,
                package.Dimensions.Width,
                isValid
            )
        );
    }
}
