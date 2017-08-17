using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Extensions;
using Abp.Modules;
using Abp.PlugIns;
using Abp.Web.Mvc.Controllers;

namespace Abp.Web.Mvc
{
    /// <summary>
    /// If web host enable plugins, you should place code:
    /// AbpBootstrapper.PlugInSources.Add(new FolderPlugInSource($"{AppDomain.CurrentDomain.BaseDirectory.EnsureEndsWith('\\')}Plugins", System.IO.SearchOption.AllDirectories));
    /// before
    /// base.Application_Start(sender, e);
    /// in global.asax
    /// </summary>
    [DependsOn(typeof(AbpWebMvcModule))]
    public class AbpWebPlugInModule : AbpModule
    {
        private readonly IAbpPlugInManager _abpPlugInManager;

        public AbpWebPlugInModule(IAbpPlugInManager abpPlugInManager)
        {
            _abpPlugInManager = abpPlugInManager;
        }

        public override void PreInitialize()
        {
            var autoLoadTypes = _abpPlugInManager.PlugInSources.GetAllModules().Select(m => m.Assembly).Distinct().ToList()
                .SelectMany(a => a.GetTypes())
                .Where(t => t != null && t.IsPublic && !t.IsAbstract
                && (typeof(IPlugInAreaRegistration).IsAssignableFrom(t))
                || typeof(IPlugInAuthorizationProvider).IsAssignableFrom(t))
                .ToList();

            //Auto Load IPlugInAreaRegistration
            foreach (var item in autoLoadTypes.Where(t => typeof(IPlugInAreaRegistration).IsAssignableFrom(t)))
            {
                if (!typeof(AreaRegistration).IsAssignableFrom(item))
                {
                    throw new AbpException($"{item.FullName} should inherits from {typeof(AreaRegistration).FullName}");
                }

                var instance = (AreaRegistration)Activator.CreateInstance(item);

                AreaRegistrationContext context = new AreaRegistrationContext(instance.AreaName, RouteTable.Routes, null);

                if (!item.Namespace.IsNullOrWhiteSpace())
                {
                    context.Namespaces.Add(item.Namespace + ".*");
                }

                instance.RegisterArea(context);
            }

            //Auto Load IPlugInAuthorizationProvider Permissions Must Load in PreInit
            foreach (var item in autoLoadTypes.Where(t => typeof(IPlugInAuthorizationProvider).IsAssignableFrom(t)))
            {
                Configuration.Authorization.Providers.Add(item);
            }
        }

        public override void PostInitialize()
        {
            ControllerBuilder.Current.SetControllerFactory(new PlugInWindsorControllerFactory(IocManager, IocManager.Resolve<IAbpPlugInManager>()));

            var autoLoadTypes = _abpPlugInManager.PlugInSources.GetAllModules().Select(m => m.Assembly).Distinct().ToList()
                .SelectMany(a => a.GetTypes())
                .Where(t => t != null && t.IsPublic && !t.IsAbstract
                && (typeof(IPlugInNavigationProvider).IsAssignableFrom(t))
                ).ToList();

            //Auto Load IPlugInNavigationProvider， Menu Should Load After the PlugIn's Host
            foreach (var item in autoLoadTypes.Where(t => typeof(IPlugInNavigationProvider).IsAssignableFrom(t)))
            {
                Configuration.Navigation.Providers.Add(item);
            }
        }
    }
}
