using PackageApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace PackageApi.Examples
{
    public class PackagesExample : IExamplesProvider<IEnumerable<Package>>
    {
        public IEnumerable<Package> GetExamples()
        {
            return new List<Package> 
            {
                    new Package("999123455432100000", new Dimensions(20000, 32, 54, 34, true)),
                    new Package("999543215432100000", new Dimensions(30000, 85, 50, 30, false))
            };
        }
    }
    public class PackageExample : IExamplesProvider<Package>
    {
        public Package GetExamples()
        {
           return new Package("999123455432100000", new Dimensions(20000, 32, 54, 34, true));
        }
    }
}
