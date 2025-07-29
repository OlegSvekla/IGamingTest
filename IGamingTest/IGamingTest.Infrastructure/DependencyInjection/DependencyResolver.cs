namespace IGamingTest.Infrastructure.DependencyInjection;

public interface IDependencyResolver
{
    T Resolve<T>()
        where T : notnull;

    object Resolve(
        Type type);
}

public abstract class DependencyResolver
    : IDependencyResolver
{
    public abstract T Resolve<T>()
        where T : notnull;

    public abstract object Resolve(
        Type type);
}
