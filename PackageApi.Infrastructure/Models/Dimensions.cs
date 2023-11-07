namespace PackageApi.Infrastructure.Models
{
    public class Dimensions
    {
        public Dimensions(string length, string height, string width)
        {
            Length = length;
            Height = height;
            Width = width;
        }
        public string Length { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
    }
}
