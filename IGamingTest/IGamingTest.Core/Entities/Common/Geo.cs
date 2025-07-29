using System.Globalization;

namespace IGamingTest.Core.Entities.Common;

public class Geo
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public Geo() { }

    public Geo(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public override string ToString() =>
        $"{Latitude.ToString(CultureInfo.InvariantCulture)}, {Longitude.ToString(CultureInfo.InvariantCulture)}";

    public string ToMapBoxString() =>
        $"{Latitude.ToString(CultureInfo.InvariantCulture)},{Longitude.ToString(CultureInfo.InvariantCulture)}";
}

