namespace WebServerTutorial.DependencyInjection;

public class HttpServerDependenciesBuilder
{
    private readonly Dictionary<Type, Func<DependencyCollection, object>> _registrations = new();

    public HttpServerDependenciesBuilder Register<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations.Add(typeof(TInterface), d => DependencyInjection.CreateInstance(typeof(TImplementation), d));

        return this;
    }

    public HttpServerDependenciesBuilder Register<TInterface>(TInterface instance) where TInterface : notnull
    {
        _registrations.Add(typeof(TInterface), _ => instance);

        return this;
    }

    public DependencyCollection Build()
    {
        return new DependencyCollection(_registrations);
    }
}