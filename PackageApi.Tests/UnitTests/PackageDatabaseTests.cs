﻿namespace PackageApi.Tests.UnitTests;

[TestClass]
public class PackageDatabaseTests
{

    [TestMethod]
    public void GetAllPackages_ReturnsAllPackages()
    {
        // Arrange
        var database = new PackageDatabase();

        // Act
        var packages = database.GetAllPackages();

        // Assert
        Assert.IsNotNull(packages);
        Assert.AreEqual(3, packages.Count()); // This has a weird behaviour as to make it easier for myself my "database" list is static so this depends on which order the tests are run at
    }
    [TestMethod]
    public void AddPackage_AddsPackageSuccessfully()
    {
        // Arrange
        var database = new PackageDatabase();
        var package = new Package("Test123", 10, new Dimensions("20", "30", "40"));

        // Act
        var result = database.AddPackage(package);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void GetPackage_ReturnsPackageIfExists()
    {
        // Arrange
        var database = new PackageDatabase();
        var package = new Package("Test123", 10, new Dimensions("20", "30", "40"));
        database.AddPackage(package);

        // Act
        var retrievedPackage = database.GetPackage("Test123");

        // Assert
        Assert.IsNotNull(retrievedPackage);
        Assert.AreEqual("Test123", retrievedPackage.KolliId);
    }

    [TestMethod]
    public void GetPackage_ReturnsNullForNonExistentPackage()
    {
        // Arrange
        var database = new PackageDatabase();

        // Act
        var retrievedPackage = database.GetPackage("NonExistentId");

        // Assert
        Assert.IsNull(retrievedPackage);
    }
}
