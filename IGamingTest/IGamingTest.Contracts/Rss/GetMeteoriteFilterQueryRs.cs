namespace IGamingTest.Core.Models;

public sealed record GetMeteoriteFilterQueryRs
(
   int? Year,
   int Count,
   double? TotalMass
);
