namespace PackageApi.Models;

public class Package
{
    public Package(string kolliId, Dimensions dimensions)
    {
        KolliId = kolliId;
        Dimensions = dimensions;
    }
    public string KolliId { get; set; }
    public Dimensions Dimensions { get; set; }
}
