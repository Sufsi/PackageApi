using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PackageApi.Infrastructure;
using PackageApi.Models;

namespace PackageApi.Facades;
public interface IPackageFacade
{
    Task<HttpResponseMessage> CreatePackage(PackageRequest package);
    Task<Package?> GetPackage(string kolliId);
    Task<IEnumerable<Package?>> GetPackages();
}
public class PackageFacade : IPackageFacade
{
    private readonly ILogger<PackageFacade> logger;
    private readonly IRepositoryFactory repositoryFactory;
    private readonly IValidator<Dimensions> dimensionsValidator;

    public PackageFacade(ILogger<PackageFacade> logger, IRepositoryFactory repositoryFactory, IValidator<Dimensions> dimensionsValidator)
    {
        this.logger = logger;
        this.repositoryFactory = repositoryFactory;
        this.dimensionsValidator = dimensionsValidator;
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

    public async Task<HttpResponseMessage> CreatePackage(PackageRequest package)
    {
        var converted = ConvertToPackage(package);

        var validationResult = await dimensionsValidator.ValidateAsync(converted.Dimensions);
        logger.LogInformation(validationResult.IsValid ? $"{package.KolliId} valid package" : $"{package.KolliId} contains properties outside of limitations");

        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var exists = await repo.Get(converted.KolliId);

        if (exists != null)
        {
            logger.LogInformation($"KolliId:{package.KolliId} already exists in the database");
            return new HttpResponseMessage(System.Net.HttpStatusCode.Conflict);
        }


        var infrastructurePackage = ConvertToInfrastructurePackage(converted, validationResult.IsValid);
        var result = await repo.Create(infrastructurePackage);

        return result ? new HttpResponseMessage(System.Net.HttpStatusCode.OK) : new HttpResponseMessage(System.Net.HttpStatusCode.FailedDependency);
    }
    private static Package? ConvertToPackage(PackageRequest packageRequest)
    {
        return new Package(
            packageRequest.KolliId,
            new Dimensions(
                packageRequest.Dimensions.Weight,
                packageRequest.Dimensions.Length,
                packageRequest.Dimensions.Height,
                packageRequest.Dimensions.Width
            ));
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
