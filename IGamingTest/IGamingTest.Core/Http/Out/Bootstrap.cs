using IGamingTest.Core.Http.Out.Mappers;

namespace IGamingTest.Core.Http.Out;

public static class Bootstrap
{
    public static IServiceCollection AddBatchHttpOut(
        this IServiceCollection services)
    {
        services.AddCoreHttpPolly();

        return services;
    }

    public static IServiceCollection AddCoreHttpPolly(
        this IServiceCollection services)
    {
        services.AddSingleton<IHttpResponseMessageToBoolMapper, HttpResponseMessageToBoolMapper>();
        services.AddSingleton<IHttpResponseMessageToDeliveredRsMapper, HttpResponseMessageToDeliveredRsMapper>();
        services.AddSingleton<IHttpRqToMessageMapper, HttpRqToMessageMapper>();
        services.AddSingleton<IHttpWithBodyRqToMessageMapper, HttpWithBodyRqToMessageMapper>();
        services.AddSingleton<IHttpResponseMessageToStreamRsMapper, HttpResponseMessageToStreamRsMapper>();

        return services;
    }
}
