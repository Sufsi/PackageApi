using FluentValidation;
using PackageApi.Infrastructure;
using PackageApi.Models;

namespace PackageApi.Facades;
public interface IPackageFacade
{
    Task<bool> CreatePackage(Package package);
    Task<Package?> GetPackage(string kolliId);
    Task<IEnumerable<Package?>> GetPackages();
}
public class PackageFacade : IPackageFacade
{
    private readonly ILogger<PackageFacade> logger;
    private readonly IRepositoryFactory repositoryFactory;
    private readonly IValidator<Dimensions> packageValidator;

    public PackageFacade(ILogger<PackageFacade> logger, IRepositoryFactory repositoryFactory, IValidator<Dimensions> packageValidator)
    {
        this.logger = logger;
        this.repositoryFactory = repositoryFactory;
        this.packageValidator = packageValidator;
    }

    public async Task<IEnumerable<Package?>> GetPackages()
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = await repo.GetAll();

        var packages = result.Select(ConvertToPackage);
        return packages;
    }

    public async Task<Package?> GetPackage(string kolliId)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = await repo.Get(kolliId);

        var package = ConvertToPackage(result);
        return package;
    }

    public async Task<bool> CreatePackage(Package package)
    {
        var validationResult = await packageValidator.ValidateAsync(package.Dimensions);
        logger.LogInformation(validationResult.IsValid ? $"{package.KolliId} valid package" : $"{package.KolliId} contains properties outside of limitations");

        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var infrastructurePackage = ConvertToInfrastructurePackage(package, validationResult.IsValid);
        var result = await repo.Create(infrastructurePackage);

        return result != null;
    }

    private static Package? ConvertToPackage(Infrastructure.Models.Package infrastructurePackage)
    {
        return infrastructurePackage != null ? new Package(
            infrastructurePackage.KolliId,
            new Dimensions(
                infrastructurePackage.Dimensions.Weight,
                infrastructurePackage.Dimensions.Length,
                infrastructurePackage.Dimensions.Height,
                infrastructurePackage.Dimensions.Width,
                infrastructurePackage.Dimensions.IsValid
            )
        ) : null;
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
