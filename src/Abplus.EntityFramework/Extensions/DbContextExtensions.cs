using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Abp.Domain.Entities;
using Abp.Reflection.Extensions;

namespace Abp.Extensions
{
    public static class DbContextExtensions
    {
        public static IEnumerable<Type> GetEntityTypes(this Type dbContextType)
        {
            return
               from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
               where
                (property.PropertyType.IsInheritsOrImplements(typeof(IDbSet<>)) ||
                property.PropertyType.IsInheritsOrImplements(typeof(DbSet<>))) &&
                property.PropertyType.GenericTypeArguments[0].IsInheritsOrImplements(typeof(IEntity<>))
               select property.PropertyType.GenericTypeArguments[0];
        }
    }
}
