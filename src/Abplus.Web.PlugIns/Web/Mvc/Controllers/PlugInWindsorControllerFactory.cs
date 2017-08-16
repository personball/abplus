using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Dependency;
using Abp.PlugIns;

namespace Abp.Web.Mvc.Controllers
{
    public class PlugInWindsorControllerFactory : WindsorControllerFactory
    {
        private readonly IAbpPlugInManager _plugInManager;

        private readonly object _locker = new object();
        private volatile Dictionary<string, ILookup<string, Type>> _plugInControllerTypesCache;

        public PlugInWindsorControllerFactory(IIocResolver iocManager, IAbpPlugInManager plugInManager) : base(iocManager)
        {
            _plugInManager = plugInManager;
        }
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            var controllerType = base.GetControllerType(requestContext, controllerName);

            if (controllerType == null)
            {
                //TODO@personball Maybe it's better to search controllerType in IoC Container?
                controllerType = GetControllerTypeFromPlugIns(requestContext, controllerName);
            }

            return controllerType;
        }
        
        private Type GetControllerTypeFromPlugIns(RequestContext requestContext, string controllerName)
        {
            InitPlugInControllerTypesCache();

            HashSet<Type> matchingTypes = new HashSet<Type>();

            ILookup<string, Type> namespaceLookup;
            if (_plugInControllerTypesCache.TryGetValue(controllerName, out namespaceLookup))
            {
                //TODO@personball Should deal with namespace.

                // this friendly name was located in the cache, now cycle through namespaces
                //if (namespaces != null)
                //{
                //    foreach (string requestedNamespace in namespaces)
                //    {
                //        foreach (var targetNamespaceGrouping in namespaceLookup)
                //        {
                //            if (IsNamespaceMatch(requestedNamespace, targetNamespaceGrouping.Key))
                //            {
                //                matchingTypes.UnionWith(targetNamespaceGrouping);
                //            }
                //        }
                //    }
                //}
                //else
                //{
                // if the namespaces parameter is null, search *every* namespace
                foreach (var namespaceGroup in namespaceLookup)
                {
                    matchingTypes.UnionWith(namespaceGroup);
                }
                //}
            }

            switch (matchingTypes.Count)
            {
                case 0: return null;
                case 1: return matchingTypes.First();
                default:
                    throw new InvalidOperationException("AmbiguousControllerTypeFromPlugIns");
            }
        }

        private void InitPlugInControllerTypesCache()
        {
            if (_plugInControllerTypesCache == null)
            {
                lock (_locker)
                {
                    if (_plugInControllerTypesCache == null)
                    {
                        var controllerTypes = _plugInManager.PlugInSources
                                .GetAllModules().Select(m => m.Assembly).Distinct().ToList()
                                .SelectMany(a => a.GetTypes())
                                .Where(t =>
                                t != null && t.IsPublic && !t.IsAbstract
                                && typeof(IController).IsAssignableFrom(t)
                                && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
                                .ToList();

                        var groupedByName = controllerTypes.GroupBy(
                          t => t.Name.Substring(0, t.Name.Length - "Controller".Length),
                          StringComparer.OrdinalIgnoreCase);
                        _plugInControllerTypesCache = groupedByName.ToDictionary(
                            g => g.Key,
                            g => g.ToLookup(t => t.Namespace ?? String.Empty, StringComparer.OrdinalIgnoreCase),
                            StringComparer.OrdinalIgnoreCase);
                    }
                }
            }
        }

    }
}
