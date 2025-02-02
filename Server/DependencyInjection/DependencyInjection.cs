using System.Reflection;

namespace Server.DependencyInjection;

public class DependencyInjection
{
    public static object CreateInstance(Type type, DependencyCollection dependencyCollection)
    {
        var constructorInfo = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();

        var parameterInfos = constructorInfo.GetParameters();

        if (parameterInfos.Length == 0)
        {
            return Activator.CreateInstance(type)!;
        }

        var parameterInstances = parameterInfos.Select(pInfo =>
        {
            if (dependencyCollection.CanResolve(pInfo.ParameterType))
            {
                return dependencyCollection.Resolve(pInfo.ParameterType);
            }

            return CreateInstance(pInfo.ParameterType, dependencyCollection);
        }).ToArray();

        return Activator.CreateInstance(type, parameterInstances)!;
    }
}