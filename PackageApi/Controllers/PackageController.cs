using MapsterMapper;
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
    private readonly IMapper mapper;

    public PackageController(ILogger<PackageController> logger, IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        this.logger = logger;
        this.repositoryFactory = repositoryFactory;
        this.mapper = mapper;
    }

    [HttpGet(Name = "Package")]
    [Route("/package")]
    public ActionResult<IEnumerable<Package>> GetPackages()
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.GetAll();

        return Ok(mapper.Map<IEnumerable<Package>>(result));
    }

    [HttpGet(Name = "Package")]
    [Route("/package/{kolliId}")]
    public ActionResult<Package> GetPackageDimensions(string kolliId)
    {
        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.Get(kolliId).Result;

        return Ok(mapper.Map<Package>(result));
    }

    [HttpPost(Name = "Package")]
    [Route("/package")]
    public ActionResult<Package> CreatePackage(Package package)
    {
        var dto = mapper.Map<Infrastructure.Models.Package>(package);

        var repo = repositoryFactory.GetRepository<Infrastructure.Models.Package>();
        var result = repo.Create(dto);

        return Ok();
    }
}
