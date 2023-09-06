namespace Contonso.SampleApi.Application.Common.Mapping;

using System.Reflection;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.ApplyMappingsFromAssembly(typeof(MappingProfile).Assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

        static bool HasInterface(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMapFrom<>);
        }

        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(HasInterface))
            .ToList();

        var argumentTypes = new[]
        {
            typeof(Profile),
        };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                var thisObj = new object[]
                {
                    this,
                };

                methodInfo.Invoke(instance, thisObj);
            }
            else
            {
                var interfaces = type.GetInterfaces()
                    .Where(HasInterface)
                    .ToList();

                if (interfaces.Count <= 0)
                {
                    continue;
                }

                foreach (var @interface in interfaces)
                {
                    var interfaceMethodInfo =
                        @interface.GetMethod(mappingMethodName, argumentTypes);

                    var thisObj = new object[]
                    {
                        this,
                    };

                    interfaceMethodInfo?.Invoke(instance, thisObj);
                }
            }
        }
    }
}
