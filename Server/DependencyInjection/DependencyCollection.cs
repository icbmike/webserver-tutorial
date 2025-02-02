namespace Server.DependencyInjection;

public class DependencyCollection(Dictionary<Type, Func<DependencyCollection, object>> registrations)
{
    public T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }

    public object Resolve(Type type)
    {
        if (registrations.TryGetValue(type, out var registration))
        {
            return registration.Invoke(this);
        }

        if (type is { IsInterface: false, IsAbstract: false })
        {
            return DependencyInjection.CreateInstance(type, this);
        }

        throw new Exception($"Can't create type {type.FullName}");
    }

    public bool CanResolve(Type type)
    {
        return registrations.ContainsKey(type);
    }
}