using FluentValidation;
using PackageApi.Models;

namespace PackageApi.Validators;

public class PackageValidator
{
    public class KolliValidator : AbstractValidator<Package>
    {
        public KolliValidator()
        {
            RuleFor(kolliId => kolliId.KolliId)
            .NotEmpty().WithMessage("KolliId cannot be empty.")
            .Length(18).WithMessage("KolliId must be 18 characters long.")
            .Matches("^[0-9]+$").WithMessage("KolliId must contain only numeric characters.")
            .Must(kolliId => kolliId.StartsWith("999")).WithMessage("KolliId must start with '999'.");
        }
    }

    public class DimensionsValidator : AbstractValidator<Package>
    {
        public DimensionsValidator()
        {
            RuleFor(package => package.Dimensions.Weight)
            .InclusiveBetween(0, 200).WithMessage("Weight must be between 0 and 200.");

            RuleFor(package => package.Dimensions.Length)
                .InclusiveBetween(0, 60).WithMessage("Length must be between 0 and 60.");

            RuleFor(package => package.Dimensions.Height)
                .InclusiveBetween(0, 60).WithMessage("Height must be between 0 and 60.");

            RuleFor(package => package.Dimensions.Width)
                .InclusiveBetween(0, 60).WithMessage("Width must be between 0 and 60.");

        }
    }
}
