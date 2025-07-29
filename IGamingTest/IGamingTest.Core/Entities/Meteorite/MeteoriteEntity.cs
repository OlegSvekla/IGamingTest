using IGamingTest.Core.Entities.Common;

namespace IGamingTest.Core.Entities.Meteorite;

public class MeteoriteEntity : Entity
{
    public string Name { get; set; } = default!;
    public string NameType { get; set; } = default!;
    public string RecClass { get; set; } = default!;
    public double Mass { get; set; } = default!;
    public string Fall { get; set; } = default!;
    public int Year { get; set; } = default!;
    public string RecLat { get; set; } = default!;
    public string RecLong { get; set; } = default!;

    public Guid GeolocationId { get; set; }
    public GeoLocationEntity Geolocation { get; set; } = default!;
}
