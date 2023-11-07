﻿namespace PackageApi.Tests.UnitTests;

[TestClass]
public class GenericRepositoryTests
{
    [TestMethod]
    public async Task GetAll_ReturnsAllPackages()
    {
        // Arrange
        var database = new PackageDatabase();
        var repository = new GenericRepository<Package>(database);

        // Act
        var packages = await repository.GetAll();

        // Assert
        Assert.IsNotNull(packages);
        Assert.AreEqual(2, packages.Count()); // Adjust the count as needed
    }
    [TestMethod]
    public async Task Create_PackageEntity_CreatesPackageSuccessfully()
    {
        // Arrange
        var database = new PackageDatabase();
        var repository = new GenericRepository<Package>(database);
        var package = new Package("Test123", 10, new Dimensions("20", "30", "40"));

        // Act
        var result = repository.Create(package);

        // Assert
        Assert.IsTrue(result.IsCompletedSuccessfully);
    }

    [TestMethod]
    public async Task Get_PackageId_ReturnsPackageIfExists()
    {
        // Arrange
        var database = new PackageDatabase();
        var repository = new GenericRepository<Package>(database);
        var package = new Package("Test123", 10, new Dimensions("20", "30", "40"));
        database.AddPackage(package);

        // Act
        var retrievedPackage = await repository.Get("Test123");

        // Assert
        Assert.IsNotNull(retrievedPackage);
        Assert.AreEqual("Test123", retrievedPackage.KolliId);
    }

    [TestMethod]
    public async Task Get_PackageId_ReturnsNullForNonExistentPackage()
    {
        // Arrange
        var database = new PackageDatabase();
        var repository = new GenericRepository<Package>(database);

        // Act
        var retrievedPackage = await repository.Get("NonExistentId");

        // Assert
        Assert.IsNull(retrievedPackage);
    }
}