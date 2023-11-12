using Microsoft.AspNetCore.Mvc;
using PackageApi.Facades;
using PackageApi.Models;

namespace PackageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private readonly ILogger<PackageController> logger;
    private readonly IPackageFacade packageFacade;

    public PackageController(ILogger<PackageController> logger, IPackageFacade packageFacade)
    {
        this.logger = logger;
        this.packageFacade = packageFacade;
    }

    [HttpGet("/package")]
    public ActionResult<IEnumerable<Package>> GetPackages()
    {
        var result = packageFacade.GetPackages();
        return Ok(result);
    }

    [HttpGet("/package/{kolliId}")]
    public ActionResult<Dimensions> GetPackageDimensions(string kolliId)
    {
        var result = packageFacade.GetPackageDimensions(kolliId);
        return Ok(result);
    }

    [HttpPost("/package")]
    public ActionResult<Package> CreatePackage(Package package)
    {
        var result = packageFacade.CreatePackage(package);
        return Ok(result);
    }
}
