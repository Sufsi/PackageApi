namespace PackageApi.Tests.UnitTests;

[TestClass]
public class PackageDatabaseTests
{
    [TestMethod]
    public void AddPackage_AddsPackageSuccessfully()
    {
        // Arrange
        var database = new PackageDatabase();
        var package = new Package("9999123450000000000", new Dimensions(100, 20, 30, 40));

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
        var package = new Package("9999123450000000000", new Dimensions(100, 20, 30, 40));
        database.AddPackage(package);

        // Act
        var retrievedPackage = database.GetPackage("9999123450000000000");

        // Assert
        Assert.IsNotNull(retrievedPackage);
        Assert.AreEqual("9999123450000000000", retrievedPackage.KolliId);
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
