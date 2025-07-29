using IGamingTest.Core.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGamingTest.Core.Entities.Meteorite;

public class GeoLocationEntity : Entity
{
    public string Type { get; set; } = default!;

    /// <summary>
    /// Waypoint geolocation
    /// </summary>
    [Column(TypeName = PostgreSqlFieldTypeConst.Json)]
    public Geo Geo { get; set; } = default!;
}
