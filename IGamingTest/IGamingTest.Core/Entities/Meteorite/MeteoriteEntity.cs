using IGamingTest.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace IGamingTest.Core.Entities.Meteorite;

[Index(nameof(Name))]
[Index(nameof(Year))]
[Index(nameof(RecClass))]
public class MeteoriteEntity : Entity
{
    public string? Name { get; set; }
    public string? NameType { get; set; }
    public string? RecClass { get; set; }
    public double? Mass { get; set; }
    public string? Fall { get; set; }
    public int? Year { get; set; }
    public string? RecLat { get; set; }
    public string? RecLong { get; set; }

    public int? GeolocationId { get; set; }
    public GeoLocationEntity? Geolocation { get; set; }
}
