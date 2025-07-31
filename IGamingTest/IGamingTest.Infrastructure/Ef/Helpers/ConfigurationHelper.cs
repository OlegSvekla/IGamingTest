using Microsoft.Extensions.Configuration;

namespace IGamingTest.Infrastructure.Ef.Helpers;

public static class ConfigurationHelper
{
    public static T Extract<T>(
        this IConfiguration config,
        string sectionKey)
        where T : class
        => config
            .GetSection(sectionKey)
            .Get<T>()
            .GetValue();
}
