namespace PackageApi.Shared.Models
{
    public class Package
    {
        public Package(string kolliId, double weight, Dimensions dimensions)
        {
            KolliId = kolliId;
            Weight = weight;
            Dimensions = dimensions;
        }
        public string KolliId { get; set; }
        public double Weight { get; set; }
        public Dimensions Dimensions { get; set; }
    }
}
