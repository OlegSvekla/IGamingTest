namespace IGamingTest.Contracts.Queries;

public class MeteoriteFilterQuery
{
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public string? RecClass { get; set; }
    public string? NameContains { get; set; }

    public int? Amount { get; set; }
    public int? Offset { get; set; }

    public string? SortBy { get; set; }
}
