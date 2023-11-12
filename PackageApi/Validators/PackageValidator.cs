using FluentValidation;
using PackageApi.Models;

namespace PackageApi.Validators;


public class PackageValidator : AbstractValidator<Package>
{
    public PackageValidator()
    {
        RuleFor(package => package.KolliId).SetValidator(new KolliValidator());

        RuleFor(package => package.Dimensions).SetValidator(new DimensionsValidator());
    }
}

public class KolliValidator : AbstractValidator<string>
{
    public KolliValidator()
    {
        RuleFor(kolliId => kolliId)
        .NotEmpty().WithMessage("KolliId cannot be empty.")
        .Length(18).WithMessage("KolliId must be 18 characters long.")
        .Matches("^[0-9]+$").WithMessage("KolliId must contain only numeric characters.")
        .Must(kolliId => kolliId.StartsWith("999")).WithMessage("KolliId must start with '999'.");
    }
}
public class DimensionsValidator : AbstractValidator<Dimensions>
{
    public DimensionsValidator()
    {
        RuleFor(package => package.Weight)
        .InclusiveBetween(0, 20000).WithMessage("Weight must be between 0 and 20000g.");

        RuleFor(package => package.Length)
            .InclusiveBetween(0, 60).WithMessage("Length must be between 0 and 60cm.");

        RuleFor(package => package.Height)
            .InclusiveBetween(0, 60).WithMessage("Height must be between 0 and 60cm.");

        RuleFor(package => package.Width)
            .InclusiveBetween(0, 60).WithMessage("Width must be between 0 and 60cm.");

    }
}
