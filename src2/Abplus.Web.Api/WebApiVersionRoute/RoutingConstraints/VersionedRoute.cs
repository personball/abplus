using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Abplus.WebApiVersionRoute.RoutingConstraints
{
    public class VersionedRoute : RouteFactoryAttribute
    {
        private static string DefaultAllowedClientVersionRange = VersionConsts.DefaultClientMinVersionString + "-" + VersionConsts.DefaultClientMaxVersionString;
        private static SysCode DefaultAllowedSysCode = SysCode.H5 | SysCode.IPhone | SysCode.Android;
        private const int DefaultAllowedApiVersion = 1;

        #region 构造函数

        #region 单参构造
        public VersionedRoute(string template)
            : this(template, DefaultAllowedApiVersion)
        {
        }
        #endregion

        #region 双参构造
        public VersionedRoute(string template, int apiVersion)
            : this(template, apiVersion, DefaultAllowedSysCode)
        {
        }

        public VersionedRoute(string template, SysCode allowedTags)
            : this(template, DefaultAllowedApiVersion, allowedTags)
        {
        }

        #endregion

        #region 三参构造
        public VersionedRoute(string template, int apiVersion, SysCode allowedTags)
            : this(template, apiVersion, allowedTags, DefaultAllowedClientVersionRange)
        {
        }
        
        public VersionedRoute(string template, int apiVersion, params string[] allowedClientVersionRangeString)
            : this(template, apiVersion, DefaultAllowedSysCode, allowedClientVersionRangeString)
        {
        }

        public VersionedRoute(string template, SysCode allowedTags, params string[] allowedClientVersionRangeString)
            : this(template, DefaultAllowedApiVersion, DefaultAllowedSysCode, allowedClientVersionRangeString)
        {
        }
        #endregion

        public VersionedRoute(string template, int apiVersion, SysCode allowedTags, params string[] allowedClientVersionRangeString)
            : base(template)
        {
            AllowedClientVersionRangeList = new List<VersionRange>();

            foreach (var rangeString in allowedClientVersionRangeString)
            {
                AllowedClientVersionRangeList.Add(VersionRange.CreateVersionRangeFromString(rangeString));
            }

            AllowedTags = allowedTags;
            ApiVersion = apiVersion;
        }

        #endregion

        public List<VersionRange> AllowedClientVersionRangeList
        {
            get;
            private set;
        }

        /// <summary>
        /// 接口的版本号
        /// </summary>
        public int ApiVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// 接口的设备区分标签
        /// </summary>
        public SysCode AllowedTags
        {
            get;
            private set;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("version", new VersionConstraint(ApiVersion, AllowedTags, AllowedClientVersionRangeList));
                return constraints;
            }
        }
    }

}