using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace PlanningPokerWebAPI.ApplicationLayer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            //var types = assembly.GetTypes()
            //    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(MapFrom<>)))
            //    .ToList();
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}
