using IGamingTest.Infrastructure;
using IGamingTest.Infrastructure.DependencyInjection;
using IGamingTest.Web.Startups;
using IGamingTest.Web.Startups.Helpers;

var builder = WebApplication.CreateBuilder(args);
var activator = new ActivatorDependencyResolver();
var app = await AppBuilder.New(activator)
    .With<Startup>()
    .With<EfStartup>()
    .With<EfPostgreSqlStartup<GameContext>>()

    .With<HttpStartup>()
    .With<HttpPollyStartup>()
    .With<HangfireStartup>()
    .With<MqLocalMediatRStartup>()

    .With<ConfigurationStartup>()
    .With<HstsStartup>()
    .With<RoutingStartup>()
    .With<SwaggerStartup>()
    .With<VersioningStartup>()
    .With<WebApiStartup>()

    .BuildAsync(builder);

await app.RunAsync();

