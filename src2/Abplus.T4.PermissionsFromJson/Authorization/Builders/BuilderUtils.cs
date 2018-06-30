using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using Newtonsoft.Json;

namespace 【YourCompany.YourProject】.Authorization
{
    public static class BuilderUtils
    {
        public static void Build(IPermissionDefinitionContext context, string name)
        {
            var asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
            var sm = asm.GetManifestResourceStream("【YourCompany.YourProject】.Authorization.Builders.Permissions." + name + ".json");
            var list = GetPermissionJson(sm);
            foreach (var item in list)
            {
                var module = context.CreatePermission(
                    item.Name, 
                    new FixedLocalizableString(item.DisplayName), 
                    multiTenancySides: item.GetMultiTenancySides());

                var children = item.GetChildren();
                BuildChildrenPermission(children, module, item.Name);
            }
        }

        //供t4调用
        public static Dictionary<string, List<PermissionConst>> GeneratePermission()
        {
            var dic = new Dictionary<string, List<PermissionConst>>();
            var asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
            var jsons =
                asm.GetManifestResourceNames()
                    .Where(c => c.StartsWith("【YourCompany.YourProject】.Authorization.Builders.Permissions") && c.EndsWith(".json"));
            foreach (var item in jsons)
            {
                var name =
                    item.Replace("【YourCompany.YourProject】.Authorization.Builders.Permissions.", "")
                        .Replace(".json", "");
                var sm = asm.GetManifestResourceStream(item);
                var list = BuildPermissionConst(GetPermissionJson(sm));
                dic.Add(name, list);
            }
            return dic;
        }

        private static List<PermissionJson> GetPermissionJson(Stream sm)
        {
            using (var reader = new StreamReader(sm))
            {
                string text = reader.ReadToEnd();
                var json = JsonConvert.DeserializeObject<List<PermissionJson>>(text).OrderBy(c => c.Order).ToList();
                return json;
            }
        }

        private static void BuildChildrenPermission(List<PermissionJson> children, Permission module, string parentName)
        {
            foreach (var item in children)
            {
                var name = $"{parentName}.{item.Name}";
                var itemModule = module.CreateChildPermission(name,
                    new FixedLocalizableString(item.DisplayName),
                    multiTenancySides: item.GetMultiTenancySides());
                var itemchildren = item.GetChildren();
                if (itemchildren.Any())
                {
                    BuildChildrenPermission(itemchildren, itemModule, name);
                }
            }
        }

        private static List<PermissionConst> BuildPermissionConst(List<PermissionJson> permissionJsons, string name = null, string value = null, string summary = null, List<PermissionConst> permissionConsts = null)
        {
            permissionConsts = permissionConsts ?? new List<PermissionConst>();
            foreach (var item in permissionJsons)
            {
                var permissionConst = new PermissionConst
                {
                    Summary = $"{summary}_{item.DisplayName}".Trim('_'),
                    Name = $"{name}_{item.Name}".Trim('_').Replace('.', '_'),
                    Value = $"{value}.{item.Name}".Trim('.').Replace('_', '.')
                };
                permissionConsts.Add(permissionConst);
                var children = item.GetChildren();
                if (children.Any())
                {
                    BuildPermissionConst(children, permissionConst.Name, permissionConst.Value, permissionConst.Summary,
                        permissionConsts);
                }
            }
            return permissionConsts;
        }
    }

    public class PermissionJson
    {
        public PermissionJson()
        {
            Order = 100;
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        [JsonProperty(PropertyName = "multiTenancySides")]
        public MultiTenancySides? MultiTenancySide { private get; set; }
        public bool DefaultPermission { private get; set; }
        public List<PermissionJson> Children { private get; set; }
        public int Order { get; set; }
        public List<int> DisableOrder { get; set; }

        public List<PermissionJson> GetChildren()
        {
            Children = Children ?? new List<PermissionJson>();
            DisableOrder = DisableOrder ?? new List<int>();
            if (DefaultPermission)
            {
                Children.Add(new PermissionJson
                {
                    Name = "Create",
                    DisplayName = "新增",
                    Order = 10,
                    DefaultPermission = false
                });
                Children.Add(new PermissionJson
                {
                    Name = "Edit",
                    DisplayName = "编辑",
                    Order = 20,
                    DefaultPermission = false
                });
                Children.Add(new PermissionJson
                {
                    Name = "Delete",
                    DisplayName = "删除",
                    Order = 30,
                    DefaultPermission = false
                });
            }
            if (MultiTenancySide.HasValue)
            {
                Children.ForEach(c =>
                {
                    c.MultiTenancySide = MultiTenancySide.Value;
                });
            }
            return Children.OrderBy(c => c.Order).Where(c => !DisableOrder.Contains(c.Order)).ToList();
        }

        public MultiTenancySides GetMultiTenancySides()
        {
            if (MultiTenancySide.HasValue)
            {
                return MultiTenancySide.Value;
            }
            return MultiTenancySides.Tenant | MultiTenancySides.Host;
        }
    }

    public class PermissionConst
    {
        public string Summary { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}