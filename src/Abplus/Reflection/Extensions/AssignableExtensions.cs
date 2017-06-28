using System;
using System.Linq;
using System.Reflection;
//from http://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class
namespace Abp.Reflection.Extensions
{
    /// <summary>
    ///     AssignableExtensions    
    /// </summary>
    public static class AssignableExtensions
    {
        /// <summary>
        ///     Checks whether <paramref name="child" /> implements/inherits <paramref name="parent" />
        /// </summary>
        /// <param name="child">Current Type</param>
        /// <param name="parent">Parent Type</param>
        /// <returns></returns>
        public static bool IsInheritsOrImplements(this Type child, Type parent)
        {
            if (child == parent)
                return false;

            if (child.IsSubclassOf(parent))
                return true;

            var parameters = parent.GetGenericArguments();

            var isParameterLessGeneric = !(parameters.Length > 0 &&
                                           ((parameters[0].Attributes & TypeAttributes.BeforeFieldInit) ==
                                            TypeAttributes.BeforeFieldInit));

            while (child != null && child != typeof(object))
            {
                var cur = GetFullTypeDefinition(child);
                if (parent == cur ||
                    (isParameterLessGeneric &&
                     cur.GetInterfaces().Select(GetFullTypeDefinition).Contains(GetFullTypeDefinition(parent))))
                {
                    return true;
                }

                if (!isParameterLessGeneric)
                {
                    if (GetFullTypeDefinition(parent) == cur && !cur.IsInterface)
                    {
                        if (VerifyGenericArguments(GetFullTypeDefinition(parent), cur))
                        {
                            if (VerifyGenericArguments(parent, child))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        //foreach (
                        //   var item in
                        //       child.GetInterfaces()
                        //           .Where(i => GetFullTypeDefinition(parent) == GetFullTypeDefinition(i)))
                        //{
                        //    if (VerifyGenericArguments(parent, item))
                        //        return true;
                        //}

                        if (child.GetInterfaces()
                            .Where(i => GetFullTypeDefinition(parent) == GetFullTypeDefinition(i))
                            .Any(item => VerifyGenericArguments(parent, item)))
                        {
                            return true;
                        }
                    }
                }
                child = child.BaseType;
            }

            return false;
        }

        private static Type GetFullTypeDefinition(Type type)
        {
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        private static bool VerifyGenericArguments(Type parent, Type child)
        {
            var childArguments = child.GetGenericArguments();
            var parentArguments = parent.GetGenericArguments();
            if (childArguments.Length == parentArguments.Length)
            {
                for (var i = 0; i < childArguments.Length; i++)
                {
                    if (childArguments[i].Assembly != parentArguments[i].Assembly ||
                        childArguments[i].Name != parentArguments[i].Name ||
                        childArguments[i].Namespace != parentArguments[i].Namespace)
                    {
                        if (!childArguments[i].IsSubclassOf(parentArguments[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
