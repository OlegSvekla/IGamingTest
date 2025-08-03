using IGamingTest.Core.Entities.Common;
using Newtonsoft.Json;
using System.Globalization;

namespace IGamingTest.Core.Models;

public class GetMeteoriteQueryRs
{
    [JsonProperty("id")]
    public string Id { get; set; } = default!;

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("nametype")]
    public string? NameType { get; set; }

    [JsonProperty("recclass")]
    public string? RecClass { get; set; }

    [JsonProperty("mass")]
    public string? Mass { get; private set; }

    [JsonProperty("fall")]
    public string? Fall { get; set; }

    [JsonProperty("year")]
    public string? Year { get; private set; }

    [JsonProperty("reclat")]
    public string? RecLat { get; set; }

    [JsonProperty("reclong")]
    public string? RecLong { get; set; }

    [JsonProperty("geolocation")]
    public GeoLocationDto? Geolocation { get; set; }

    [JsonIgnore]
    public double MassValue
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Mass))
                return 0;

            var normalized = Mass.Replace(",", ".");

            if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                return value;

            return 0;
        }
    }

    [JsonIgnore]
    public int YearValue
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Year))
                return 0;

            if (DateTime.TryParse(Year, out var dt))
                return dt.Year;

            var digits = new string(Year.TakeWhile(char.IsDigit).ToArray());
            return int.TryParse(digits, out var y) ? y : 0;
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
