using Microsoft.AspNetCore.Mvc;
using PackageApi.Infrastructure;
using PackageApi.Shared.Models;

namespace PackageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private readonly ILogger<PackageController> logger;
    private readonly IRepositoryFactory repositoryFactory;

    public PackageController(ILogger<PackageController> logger, IRepositoryFactory repositoryFactory)
    {
        this.logger = logger;
        this.repositoryFactory = repositoryFactory;
    }

    [HttpGet("/package")]
    public ActionResult<IEnumerable<Package>> GetPackages()
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.GetAll().Result;

        var packages = result.Select(item => new Package(item.KolliId, item.Weight, new Dimensions(item.Dimensions.Length, item.Dimensions.Height, item.Dimensions.Width)));
        return Ok(packages);
    }

    [HttpGet("/package/{kolliId}")]
    public ActionResult<Package> GetPackageDimensions(string kolliId)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.Get(kolliId).Result;

        var package = new Package(result.KolliId, result.Weight, new Dimensions(result.Dimensions.Length, result.Dimensions.Height, result.Dimensions.Width));
        return Ok(package);
    }

    [HttpPost("/package")]
    public ActionResult<Package> CreatePackage(Package package)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.Create(new Infrastructure.Models.Package(package.KolliId, package.Weight, new Infrastructure.Models.Dimensions(package.Dimensions.Length, package.Dimensions.Height, package.Dimensions.Width)));

        return Ok(result);
    }
}
