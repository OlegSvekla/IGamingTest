namespace IGamingTest.Core.Serializers;

public static class Bootstrap
{
    public static IServiceCollection AddBatchSerializers(
        this IServiceCollection services)
    {
        services.AddSingleton<ISerializer<string>, NewtonsoftSerializer>();
        services.AddSingleton<ISerializer<byte[]>, NewtonsoftSerializer>();
        services.AddSingleton<ISchemaGenerator, NJsonSchemaGenerator>();
        return services;
    }
}
