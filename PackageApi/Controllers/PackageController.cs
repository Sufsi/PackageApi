using Microsoft.AspNetCore.Mvc;
using PackageApi.Facades;
using PackageApi.Models;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Swashbuckle.AspNetCore.Filters;
using PackageApi.Examples;

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
    [SwaggerRequestExample(typeof(IEnumerable<Package>), typeof(PackagesExample))]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "No packages was found in the database")]
    public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
    {
        var result = await packageFacade.GetPackages();
        if (result == null)
        {
            logger.LogInformation($"No packages was found in the database");
            return NotFound($"No packages was found in the database");
        }

        return Ok(result);
    }

    [HttpGet("/package/{kolliId}")]
    [SwaggerRequestExample(typeof(Package), typeof(PackageExample))]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "KolliId did not pass the validation")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "KolliId was not found in the database")]
    public async Task<ActionResult<Package>> GetPackage([Required][FromRoute] string kolliId)
    {
        var validate = await validator.ValidateAsync(kolliId);

        if (!validate.IsValid)
        {
            logger.LogInformation($"KolliId is not valid: {string.Join(", ", validate.Errors.Select(x => x.ErrorMessage))}");
            return BadRequest($"KolliId is not valid: {string.Join(", ", validate.Errors.Select(x => x.ErrorMessage))}");
        }

        var result = await packageFacade.GetPackage(kolliId);

        if (result == null)
        {
            logger.LogInformation($"KolliId:{kolliId} was not found in the database");
            return NotFound($"KolliId:{kolliId} was not found in the database");
        }


        return Ok(result);
    }

    [HttpPost("/package")]
    [SwaggerRequestExample(typeof(PackageRequest), typeof(PackageRequestExample))]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Package is required")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "KolliId already exists in the database")]
    public async Task<ActionResult<HttpResponseMessage>> CreatePackage([Required][FromBody]PackageRequest package)
    {
        var validate = await validator.ValidateAsync(package.KolliId);

        if (!validate.IsValid)
        {
            logger.LogInformation($"KolliId is not valid: {string.Join(", ", validate.Errors.Select(x => x.ErrorMessage))}");
            return BadRequest($"KolliId is not valid: {string.Join(", ", validate.Errors.Select(x => x.ErrorMessage))}");
        }

        var result = await packageFacade.CreatePackage(package);
        return result.IsSuccessStatusCode ? Ok() : Conflict($"KolliId:{package.KolliId} already exists in the database");
    }
}
