using IGamingTest.Core.Entities.Common;
using Newtonsoft.Json;

namespace IGamingTest.Core.Models;

public class GetMeteoriteQueryRs
{
    public string Id { get; set; } = default!;

    public string? Name { get; set; }
    public string? NameType { get; set; }
    public string? RecClass { get; set; }
    public string? Mass { get; private set; }
    public string? Fall { get; set; }
    public string? Year { get; private set; }
    public string? RecLat { get; set; }
    public string? RecLong { get; set; }
    public GeoLocationDto? Geolocation { get; set; }

    [JsonIgnore]
    public double MassValue => double.TryParse(Mass, out var m) ? m : 0;

    [JsonIgnore]
    public int YearValue
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Year))
                return 0;

            if (DateTime.TryParse(Year, out var dt))
                return dt.Year;

            return 0;
        }
    }

    [JsonIgnore]
    public int IdValue => int.TryParse(Id, out var dt) ? dt : 0;
}

public class GeoLocationDto
{
    public string? Type { get; set; }
    public IReadOnlyCollection<decimal>? Coordinates { get; set; }

    [JsonIgnore]
    public Geo? Geo
    {
        get
        {
            if (Coordinates != null && Coordinates.Count >= 2)
            {
                return new Geo
                {
                    Longitude = Coordinates.ElementAt(0),
                    Latitude = Coordinates.ElementAt(1)
                };
            }
            return null!;
        }
    }
}
