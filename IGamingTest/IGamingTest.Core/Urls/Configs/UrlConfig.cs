namespace IGamingTest.Core.Urls.Configs;

public class UrlConfig
{
     /// <summary>
     /// The URL of the external resource (API or file) from which the application fetches meteorite data.
     /// For example, this could be a link to a JSON file containing meteorite information,
     /// or the endpoint of an API providing this data.
     /// </summary>
    public string MeteoritesUrl { get; set; } = default!;
}
