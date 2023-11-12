using FluentValidation;
using PackageApi.Infrastructure;
using PackageApi.Models;
using PackageApi.Services;

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
    private readonly IMapperHelper mapperHelper;
    private readonly IValidator<Dimensions> dimensionsValidator;

    public PackageFacade(ILogger<PackageFacade> logger, IRepositoryFactory repositoryFactory, IMapperHelper mapperHelper, IValidator<Dimensions> dimensionsValidator)
    {
        this.logger = logger;
        this.repositoryFactory = repositoryFactory;
        this.mapperHelper = mapperHelper;
        this.dimensionsValidator = dimensionsValidator;
    }

    public async Task<IEnumerable<Package?>> GetPackages()
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = await repo.GetAll();

        var packages = result.Select(mapperHelper.ConvertToPackage);
        return packages;
    }

    public async Task<Package?> GetPackage(string kolliId)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = await repo.Get(kolliId);

        var package = mapperHelper.ConvertToPackage(result);
        return package;
    }

    public async Task<HttpResponseMessage> CreatePackage(PackageRequest package)
    {
        var converted = mapperHelper.ConvertToPackage(package);

        var validationResult = await dimensionsValidator.ValidateAsync(converted.Dimensions);
        logger.LogInformation(validationResult.IsValid ? $"{package.KolliId} valid package" : $"{package.KolliId} contains properties outside of limitations");

        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var exists = await repo.Get(converted.KolliId);

        if (exists != null)
        {
            logger.LogInformation($"KolliId:{package.KolliId} already exists in the database");
            return new HttpResponseMessage(System.Net.HttpStatusCode.Conflict);
        }


        var infrastructurePackage = mapperHelper.ConvertToInfrastructurePackage(converted, validationResult.IsValid);
        var result = await repo.Create(infrastructurePackage);

        return result ? new HttpResponseMessage(System.Net.HttpStatusCode.OK) : new HttpResponseMessage(System.Net.HttpStatusCode.FailedDependency);
    }
}
