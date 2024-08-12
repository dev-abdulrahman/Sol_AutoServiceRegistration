using Sol_AutoServiceRegistration.Attributes;
using Sol_AutoServiceRegistration.Implementation;
using Sol_AutoServiceRegistration.Interfaces;
using System.Reflection;

namespace Sol_AutoServiceRegistration
{
    public static class ServiceRegistrationDI
    {
        public static void AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();
            var interfaces = types.Where(t => t.IsInterface && t.Namespace == "Sol_AutoServiceRegistration.Interfaces").ToList();
            var implementations = types.Where(t => t.IsClass && t.Namespace == "Sol_AutoServiceRegistration.Implementation").ToList();

            foreach (var @interface in interfaces)
            {
                //IsAssignableFrom =>
                //Determines whether an instance of a specified type c can be assigned to a variable of the current type.
                // for example IDemo demo = new Demo();
                var implementation = implementations.FirstOrDefault(impl => @interface.IsAssignableFrom(impl));
                if (implementation != null)
                {
                    services.AddScoped(@interface, implementation);
                }
            }
        }

        public static void AddAttributeBasedServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(t => t.GetCustomAttribute<ServiceAttribute>() != null);

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<ServiceAttribute>();
                var lifetime = attribute.Lifetime;
                var @interface = type.GetInterfaces().FirstOrDefault();

                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient(@interface, type);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(@interface, type);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(@interface, type);

                        break;
                }
            }
        }
    }
}
