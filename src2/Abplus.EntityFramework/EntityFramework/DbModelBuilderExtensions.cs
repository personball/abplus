using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Abp.EntityFramework
{
    public static class DbModelBuilderExtensions
    {
        public static void AutoRegisterTypeConfigurations(this DbModelBuilder modelBuilder, Assembly assembly, params Type[] baseTypes)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && baseTypes.Contains(type.BaseType.GetGenericTypeDefinition()));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
