﻿namespace PackageApi.Models;

public class Package
{
    /// <summary>
    /// Package
    /// </summary>
    /// <param name="kolliId">The KolliId of the package.</param>
    /// <param name="dimensions">The dimensions of the package</param>
    public Package(string kolliId, Dimensions dimensions)
    {
        KolliId = kolliId;
        Dimensions = dimensions;
    }
    /// <summary>
    /// The KolliId of the package
    /// </summary>
    public string KolliId { get; set; }
    /// <summary>
    /// The dimensions of the package
    /// </summary>
    public Dimensions Dimensions { get; set; }
}

public class PackageRequest
{
    /// <summary>
    /// Package
    /// </summary>
    /// <param name="kolliId">The KolliId of the package</param>
    /// <param name="dimensions">The dimensions of the package</param>
    public PackageRequest(string kolliId, DimensionsRequest dimensions)
    {
        KolliId = kolliId;
        Dimensions = dimensions;
    }
    /// <summary>
    /// The KolliId of the package
    /// </summary>
    public string KolliId { get; set; }
    /// <summary>
    /// The dimensions of the package
    /// </summary>
    public DimensionsRequest Dimensions { get; set; }
}
