using IGamingTest.Core.Entities.Common;
using Newtonsoft.Json;

namespace IGamingTest.Core.Models;

public class MeteoriteDto
{
    public string Id { get; set; }

    public string Name { get; set; }
    public string NameType { get; set; }
    public string RecClass { get; set; }
    public string Mass { get; private set; }
    public string Fall { get; set; }
    public string Year { get; private set; }
    public string RecLat { get; set; }
    public string RecLong { get; set; }
    public GeoLocationDto Geolocation { get; set; }

    [JsonIgnore]
    public double MassValue => double.TryParse(Mass, out var m) ? m : 0;

    [JsonIgnore]
    public int YearValue => DateTime.TryParse(Year, out var dt) ? dt.Year : 0;

    public MeteoriteDto() { }

    public MeteoriteDto(
        string name,
        string id,
        string nameType,
        string recClass,
        string mass,
        string fall,
        string year,
        string recLat,
        string recLong,
        GeoLocationDto geolocation)
    {
        Name = name;
        Id = id;
        NameType = nameType;
        RecClass = recClass;
        Fall = fall;
        RecLat = recLat;
        RecLong = recLong;
        Geolocation = geolocation;

        Mass = mass;
        Year = year;
    }
}

public class GeoLocationDto
{
    public string Type { get; set; }
    public List<double> Coordinates { get; set; }
}
