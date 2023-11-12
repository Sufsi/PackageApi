namespace PackageApi.Models;

public class Dimensions
{
    /// <summary>
    /// The dimensions of the package
    /// </summary>
    public Dimensions(double weight, int length, int height, int width, bool isValid = true)
    {
        Weight = weight;
        Length = length;
        Height = height;
        Width = width;
        IsValid = isValid;
    }
    /// <summary>
    /// Weight of the package in grams. Max 20000g
    /// </summary>
    public double Weight { get; }
    /// <summary>
    /// Length of the package in cm. Max 60cm
    /// </summary>
    public int Length { get; }
    /// <summary>
    /// Height of the package in cm. Max 60cm
    /// </summary>
    public int Height { get; }
    /// <summary>
    /// Width of the package in cm. Max 60cm
    /// </summary>
    public int Width { get; }
    public bool IsValid { get; }

}

public class DimensionsRequest
{
    /// <summary>
    /// The dimensions of the package
    /// </summary>
    public DimensionsRequest(double weight, int length, int height, int width)
    {
        Weight = weight;
        Length = length;
        Height = height;
        Width = width;
    }
    /// <summary>
    /// Weight of the package in grams. Max 20000g
    /// </summary>
    public double Weight { get; }
    /// <summary>
    /// Length of the package in cm. Max 60cm
    /// </summary>
    public int Length { get; }
    /// <summary>
    /// Height of the package in cm. Max 60cm
    /// </summary>
    public int Height { get; }
    /// <summary>
    /// Width of the package in cm. Max 60cm
    /// </summary>
    public int Width { get; }
}
