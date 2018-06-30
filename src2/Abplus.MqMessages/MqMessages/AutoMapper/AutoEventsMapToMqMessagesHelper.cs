using System.Linq;
using System.Reflection;
using Abp.MqMessages.Handlers;
using AutoMapper;

namespace Abp.MqMessages.AutoMapper
{
    public static class AutoEventsMapToMqMessagesHelper
    {
        public static void CreateEventsToMqMessagesMappings(this IMapperConfigurationExpression mapper, Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null
                && type.BaseType.IsGenericType
                && (type.BaseType.GetGenericTypeDefinition() == typeof(EventDataPublishHandlerBase<,>)));

            foreach (var type in typesToRegister)
            {
                var genericArgs = type.BaseType.GetGenericArguments();
                if (genericArgs.Length > 1 && !genericArgs[0].BaseType.IsGenericType)
                {
                    mapper.CreateMap(genericArgs[0], genericArgs[1]);
                }
            }
        }
    }
}
