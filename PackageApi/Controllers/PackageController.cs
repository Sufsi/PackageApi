using Microsoft.AspNetCore.Mvc;
using PackageApi.Facades;
using PackageApi.Models;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace PackageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private readonly ILogger<PackageController> logger;
    private readonly IPackageFacade packageFacade;
    private readonly IValidator<string> validator;

    public PackageController(ILogger<PackageController> logger, IPackageFacade packageFacade, IValidator<string> validator)
    {
        this.logger = logger;
        this.packageFacade = packageFacade;
        this.validator = validator;
    }

    [HttpGet("/package")]
    public ActionResult<IEnumerable<Package>> GetPackages()
    {
        var result = packageFacade.GetPackages();
        return Ok(result);
    }

    [HttpGet("/package/{kolliId}")]
    public ActionResult<Package> GetPackageDimensions([Required][FromRoute] string kolliId)
    {
        var validate = validator.Validate(kolliId);

        if (!validate.IsValid)
        {
            return BadRequest($"KolliId is not valid: {string.Join(", ",validate.Errors.Select(x => x.ErrorMessage))}");
        }

        var result = packageFacade.GetPackage(kolliId);
        return Ok(result);
    }

    [HttpPost("/package")]
    public ActionResult<Package> CreatePackage(Package package)
    {
        var result = packageFacade.CreatePackage(package);
        return Ok(result);
    }
}
