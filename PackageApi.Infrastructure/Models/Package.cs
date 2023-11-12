namespace PackageApi.Infrastructure.Models;

public class Package : Entity
{
    public Package(string kolliId, Dimensions dimensions)
    {
        KolliId = kolliId;
        Dimensions = dimensions;
    }
    public string KolliId { get; }
    public Dimensions Dimensions { get; }
}
