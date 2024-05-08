using ReviewHubAPI.Mappers.Interface;
using System.Reflection;

namespace ReviewHubAPI.Extensions;

/// <summary>
/// Extension-klasse som håndterer automatisk registrering av alle mapper-klasser i assembly.
/// Registrerer alle mapper-klasser i den kjørende applikasjonens assembly som implementerer IMapper-interface.
/// Dette forenkler dependency injection ved å legge til alle mappere i tjenestekontaineren som scoped services.
/// </summary>

public static class MapperRegistrationExtension
{
    public static void RegisterMappers(this WebApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly(); 

        var mapperTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
            .ToList();

        foreach (var mapperType in mapperTypes)
        {
            var interfaceType = mapperType.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IMapper<,>));
            builder.Services.AddScoped(interfaceType, mapperType);
        }
    }
}
