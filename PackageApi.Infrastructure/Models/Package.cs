namespace PackageApi.Infrastructure.Models;

public class Package : Entity
{
    public Package(string kolliId, double weight, Dimensions dimensions)
    {
        Id = new Guid();
        KolliId = kolliId;
        Weight = weight;
        Dimensions = dimensions;
    }
    public string KolliId { get; set; }
    public double Weight { get; set; }
    public Dimensions Dimensions { get; set; }
}
