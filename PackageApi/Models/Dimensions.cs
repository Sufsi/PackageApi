namespace PackageApi.Models;

public class Dimensions
{
    /// <summary>
    /// The dimensions of the package
    /// </summary>
    /// <param name="weight">Weight of the package in grams. Max 20000g</param>
    /// <param name="length">Length of the package in cm. Max 60cm</param>
    /// <param name="height">Height of the package in cm. Max 60cm</param>
    /// <param name="width">Width of the package in cm. Max 60cm</param>
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

public class DimensionsRequest
{
    /// <summary>
    /// The dimensions of the package
    /// </summary>
    /// <param name="weight">Weight of the package in grams. Max 20000g</param>
    /// <param name="length">Length of the package in cm. Max 60cm</param>
    /// <param name="height">Height of the package in cm. Max 60cm</param>
    /// <param name="width">Width of the package in cm. Max 60cm</param>
    public DimensionsRequest(double weight, int length, int height, int width)
    {
        Weight = weight;
        Length = length;
        Height = height;
        Width = width;
    }
    public double Weight { get; }
    public int Length { get; }
    public int Height { get; }
    public int Width { get; }
}
