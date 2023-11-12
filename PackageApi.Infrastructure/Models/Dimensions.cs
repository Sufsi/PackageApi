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
    public double Weight { get; }
    public int Length { get; }
    public int Height { get; }
    public int Width { get; }
    public bool IsValid { get; }
}
