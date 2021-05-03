using System.Linq;
using Abp.Extensions;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    public static class MenuItemDefinitionPlugInExtensions
    {
        public static MenuItemDefinition GetOrCreatePlugInsRootMenu(this MenuDefinition menu, string url, string icon, string plugInMenuName = "PlugIns", string fixedDisplayName = null, string requiredPermission = null)
        {
            if (plugInMenuName.IsNullOrWhiteSpace())
            {
                plugInMenuName = "PlugIns";
            }

            if (fixedDisplayName.IsNullOrWhiteSpace())
            {
                fixedDisplayName = "PlugIns";
            }

            var plugInRoot = menu.Items.FirstOrDefault(i => i.Name == plugInMenuName);
            if (plugInRoot == null)
            {
                plugInRoot = new MenuItemDefinition(
                    plugInMenuName,
                    new FixedLocalizableString(fixedDisplayName),
                    url: url,
                    icon: icon
                    //permissionDependency:
                    //requiredPermissionName: requiredPermission
                    );

                menu.AddItem(plugInRoot);
            }

            return plugInRoot;
        }
    }
}
