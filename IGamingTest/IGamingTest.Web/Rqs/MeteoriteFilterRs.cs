using FluentValidation;
using IGamingTest.Core.Attributes;
using IGamingTest.Core.Enums;

namespace IGamingTest.Web.Rqs;

public class GetMeteoriteFilterRq
{
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public string? RecClass { get; set; }
    public string? NameContains { get; set; }

    public int? Amount { get; set; }
    public int? Offset { get; set; }

    [property: FromEnum(typeof(SortByMeteoriteEnum))]
    public string? SortBy { get; set; }

    [property: FromEnum(typeof(SortDirectionEnum))]
    public string? SortDirection { get; set; }
}

public class MeteoriteFilterRqValidator : AbstractValidator<GetMeteoriteFilterRq>
{
    public MeteoriteFilterRqValidator()
    {
        RuleFor(x => x.Amount)
            .InclusiveBetween(Infrastructure.Consts.MinAmount, Infrastructure.Consts.MaxAmount)
            .When(x => x.Amount.HasValue);

        RuleFor(x => x.Offset)
            .InclusiveBetween(Infrastructure.Consts.MinOffset, Infrastructure.Consts.MaxOffset)
            .When(x => x.Offset.HasValue);

        RuleFor(x => x.YearTo)
            .LessThanOrEqualTo(DateTime.UtcNow.Year)
            .WithMessage($"YearTo must be less than or equal to the current year ({DateTime.UtcNow.Year})")
            .When(x => x.YearTo.HasValue);

        RuleFor(x => x.YearFrom)
            .LessThanOrEqualTo(x => x.YearTo!.Value)
            .WithMessage("YearFrom must be less than or equal to YearTo")
            .When(x => x.YearFrom.HasValue && x.YearTo.HasValue);

        RuleFor(x => x.SortBy)
            .IsEnumName(typeof(SortByMeteoriteEnum), false)
            .When(x => !string.IsNullOrEmpty(x.SortBy));

        RuleFor(x => x.SortDirection)
            .IsEnumName(typeof(SortDirectionEnum), false)
            .When(x => !string.IsNullOrEmpty(x.SortDirection));

        RuleFor(x => x.SortBy)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.SortDirection))
            .WithMessage("SortBy must be provided when SortDirection is specified.");

        RuleFor(x => x.SortDirection)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.SortBy))
            .WithMessage("SortDirection must be provided when SortBy is specified.");
    }
}
