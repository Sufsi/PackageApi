using Microsoft.AspNetCore.Mvc;
using PackageApi.Shared.Models;

namespace PackageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly ILogger<PackageController> _logger;

        public PackageController(ILogger<PackageController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Package")]
        [Route("/package")]
        public IEnumerable<Package> GetPackages()
        {
            return new List<Package>();
        }

        [HttpGet(Name = "Package")]
        [Route("/package/{kolliId}")]
        public Package GetPackageDimensions(string kolliId)
        {
            return new(default, default, default);
        }

        [HttpPost(Name = "Package")]
        [Route("/package")]
        public Package CreatePackage()
        {
            return new(default, default, default);
        }
    }
}
