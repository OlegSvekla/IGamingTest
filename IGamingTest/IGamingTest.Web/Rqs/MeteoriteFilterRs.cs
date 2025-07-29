using FluentValidation;
using IGamingTest.Core.Attributes;
using IGamingTest.Core.Enums;

namespace IGamingTest.Web.Rqs;

public class MeteoriteFilterRq
{
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public string? RecClass { get; set; }
    public string? NameContains { get; set; }

    public int? Amount { get; set; }
    public int? Offset { get; set; }

    [property: FromEnum(typeof(SortByMeteoriteEnum))]
    public string? SortBy { get; set; }
}

public class MeteoriteFilterRqValidator : AbstractValidator<MeteoriteFilterRq>
{
    public MeteoriteFilterRqValidator()
    {
        RuleFor(x => x.Amount)
            .InclusiveBetween(Core.Models.Consts.MinAmount, Core.Models.Consts.MaxAmount)
            .When(x => x.Amount.HasValue);

        RuleFor(x => x.Offset)
            .InclusiveBetween(Core.Models.Consts.MinOffset, Core.Models.Consts.MaxOffset)
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
            .IsEnumName(typeof(SortByMeteoriteEnum), caseSensitive: false)
            .When(x => !string.IsNullOrEmpty(x.SortBy));
    }
}
