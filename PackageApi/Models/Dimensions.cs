namespace PackageApi.Models;

public class Dimensions
{
    public Dimensions(double weight, int length, int height, int width)
    {
        Weight = weight;
        Length = length;
        Height = height;
        Width = width;
    }
    public double Weight { get; set; }
    public int Length { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
}
