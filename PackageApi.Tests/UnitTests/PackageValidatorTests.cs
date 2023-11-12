using PackageApi.Validators;
using Package = PackageApi.Models.Package;
using Dimensions = PackageApi.Models.Dimensions;

namespace PackageApi.Tests.UnitTests;

[TestClass]
public class PackageValidatorTests
{
    [TestMethod]
    public void KolliValidator_ShouldHaveValidationError_WhenKolliIdIsEmpty()
    {
        // Arrange
        var validator = new KolliValidator();

        // Act
        var result = validator.Validate(string.Empty);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "KolliId cannot be empty."));
    }

    [TestMethod]
    public void KolliValidator_ShouldHaveValidationError_WhenKolliIdLengthIsNot18()
    {
        // Arrange
        var validator = new KolliValidator();

        // Act
        var result = validator.Validate("1234567890123456789");

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "KolliId must be 18 characters long."));
    }

    [TestMethod]
    public void KolliValidator_ShouldHaveValidationError_WhenKolliIdContainsNonNumericCharacters()
    {
        // Arrange
        var validator = new KolliValidator();

        // Act
        var result = validator.Validate("123abc456def789ghi");

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "KolliId must contain only numeric characters."));
    }

    [TestMethod]
    public void KolliValidator_ShouldHaveValidationError_WhenKolliIdDoesNotStartWith999()
    {
        // Arrange
        var validator = new KolliValidator();

        // Act
        var result = validator.Validate("123456789012345678");

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "KolliId must start with '999'."));
    }

    [TestMethod]
    public void DimensionsValidator_ShouldHaveValidationError_WhenWeightIsLessThan0()
    {
        // Arrange
        var validator = new DimensionsValidator();
        var package = new Package("9999123456789012345678", new Dimensions(-1, 0, 0, 0));

        // Act
        var result = validator.Validate(package.Dimensions);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "Weight must be between 0 and 20000."));
    }

    // Similar tests for other DimensionValidator rules...

    [TestMethod]
    public void DimensionsValidator_ShouldNotHaveValidationError_WhenDimensionsAreValid()
    {
        // Arrange
        var validator = new DimensionsValidator();
        var package = new Package("9999123456789012345678", new Dimensions(50, 30, 40, 20));

        // Act
        var result = validator.Validate(package.Dimensions);

        // Assert
        Assert.IsTrue(result.IsValid);
        Assert.IsFalse(result.Errors.Any());
    }
}
