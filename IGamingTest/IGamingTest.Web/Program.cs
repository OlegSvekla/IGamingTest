using IGamingTest.Infrastructure.DependencyInjection;
using IGamingTest.Web;
using IGamingTest.Web.Startups;
using IGamingTest.Web.Startups.Helpers;

var builder = WebApplication.CreateBuilder(args);
var activator = new ActivatorDependencyResolver();
var app = await AppBuilder.New(activator)
    .With<Startup>()
    .With<HangfireStartup>()
    .With<ConfigurationStartup>()
    .With<HstsStartup>()
    .With<RoutingStartup>()
    .With<SwaggerStartup>()
    .With<VersioningStartup>()
    .With<WebApiStartup>()

    //.With<UseJobsStartup>()

    .BuildAsync(builder);

await app.RunAsync();

