namespace PackageApi.Infrastructure.Models;

public class Dimensions
{
    public Dimensions(double weight, int length, int height, int width, bool isValid = true)
    {
        Weight = weight;
        Length = length;
        Height = height;
        Width = width;
        IsValid = isValid;
    }
    public double Weight { get; set; }
    public int Length { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public bool IsValid { get; set; }
}
