namespace PackageApi.Tests.UnitTests;

[TestClass]
public class GenericRepositoryTests
{
    [TestMethod]
    public async Task Create_PackageEntity_CreatesPackageSuccessfully()
    {
        // Arrange
        var database = new PackageDatabase();
        var repository = new GenericRepository<Package>(database);
        var package = new Package("9999123450000000000", new Dimensions(200, 32, 30, 40));

        // Act
        var result = await repository.Create(package);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task Get_PackageId_ReturnsPackageIfExists()
    {
        // Arrange
        var database = new PackageDatabase();
        var repository = new GenericRepository<Package>(database);
        var package = new Package("9999999912345000000000012345", new Dimensions(200, 32, 30, 40));
        database.AddPackage(package);

        // Act
        var retrievedPackage = await repository.Get("9999123450000000000");

        // Assert
        Assert.IsNotNull(retrievedPackage);
        Assert.AreEqual("9999123450000000000", retrievedPackage.KolliId);
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
