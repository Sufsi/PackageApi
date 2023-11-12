using PackageApi.Models;

namespace PackageApi.Services;

public interface IMapperHelper
{
    Infrastructure.Models.Package ConvertToInfrastructurePackage(Package package, bool isValid);
    Package? ConvertToPackage(Infrastructure.Models.Package infrastructurePackage);
    Package? ConvertToPackage(PackageRequest packageRequest);
}
public class MapperHelper : IMapperHelper
{
    public Package ConvertToPackage(PackageRequest packageRequest)
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

    public Package? ConvertToPackage(Infrastructure.Models.Package infrastructurePackage)
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

    public Infrastructure.Models.Package ConvertToInfrastructurePackage(Package package, bool isValid)
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
