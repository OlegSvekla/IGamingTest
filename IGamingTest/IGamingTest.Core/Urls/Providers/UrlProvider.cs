using IGamingTest.Core.Urls.Configs;
using Microsoft.Extensions.Options;

namespace IGamingTest.Core.Urls.Providers;

public interface IUrlProvider
{
    UrlConfig GetConfigInfo();
}

public class UrlProvider(
    IOptions<UrlConfig> options
    ) : IUrlProvider
{
    private readonly UrlConfig config = options.Value;

    public UrlConfig GetConfigInfo()
        => config;
}
