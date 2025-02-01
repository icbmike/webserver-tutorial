using System.Reflection;

namespace WebServerTutorial.Server;

public class DependencyInjection
{
    public static object? CreateInstance(Type type)
    {
        var constructorInfo = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();

        var parameterInfos = constructorInfo.GetParameters();

        if (parameterInfos.Length == 0) return Activator.CreateInstance(type);

        var parameterInstances = parameterInfos.Select(pInfo => CreateInstance(pInfo.ParameterType)).ToArray();

        return Activator.CreateInstance(type, parameterInstances);
    }
}