namespace PackageApi.Shared.Models;

public class Package
{
    public string KolliId { get; set; }
    public double Weight { get; set; }
    public Dimensions Dimensions { get; set; }
}
