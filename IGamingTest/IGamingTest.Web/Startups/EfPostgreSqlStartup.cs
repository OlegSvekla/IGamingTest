using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;
using IGamingTest.Ef.Configs;
using IGamingTest.Infrastructure;
using IGamingTest.Infrastructure.Ef.Helpers;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IGamingTest.Web.Startups;

public class EfPostgreSqlStartup<TEfContext> : ServiceStartup
    where TEfContext : DbContext, IEfContext
{
    public override ServiceRegistrationOrderEnum ServiceRegistrationOrder
        => ServiceRegistrationOrderEnum.Medium;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder appBuilder)
    {
        var dbConfig = appBuilder.Configuration.Extract<DbConfig>(Infrastructure.Consts.DbConfigSectionKey);

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(dbConfig.ConnectionString);
        dataSourceBuilder.EnableDynamicJson();
        dataSourceBuilder.UseJsonNet();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<IEfContext, TEfContext>(
            optionsAction: options => options
                .UseNpgsql(
                    dataSource: dataSource,
                    npgsqlOptionsAction: sqlOption => sqlOption
                        .EnableRetryOnFailure())
                .EnableSensitiveDataLogging());
        return ValueTask.FromResult(services);
    }
}
