using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Abplus.WebApiVersionRoute.RoutingConstraints
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        public static Version DefaultClientMinVersion = new Version(VersionConsts.DefaultClientMinVersionString);

        public static Version DefaultClientMaxVersion = new Version(VersionConsts.DefaultClientMaxVersionString);

        private static SysCode DefaultSysCode = SysCode.H5;//请求端未声明SysCode时，默认为H5

        public VersionConstraint(int allowedVersion, SysCode allowedSysCode, List<VersionRange> allowedClientVersionRangeList)
        {
            AllowedVersion = allowedVersion;
            AllowedSysCode = allowedSysCode;
            AllowedClientVersionRangeList = allowedClientVersionRangeList;
        }

        public List<VersionRange> AllowedClientVersionRangeList
        {
            get;
            private set;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public SysCode AllowedSysCode
        {
            get;
            private set;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            return MatchClientVersion(request, routeDirection)
                && MatchApiVersion(request, routeDirection)
                && MatchSysCode(request, routeDirection);
        }

        private bool MatchSysCode(HttpRequestMessage request, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                var sysCode = GetSysCodeHeader(request) ?? DefaultSysCode;//请求方未声明则默认为H5
                return (sysCode & AllowedSysCode) == sysCode;
            }

            return false;
        }

        private bool MatchApiVersion(HttpRequestMessage request, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                int apiVersion = GetApiVersionHeader(request) ?? VersionConsts.DefaultApiMinVersion;//请求方未声明，则为1

                return (apiVersion == AllowedVersion);
            }

            return false;
        }

        private bool MatchClientVersion(HttpRequestMessage request, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                if (!AllowedClientVersionRangeList.Any())
                {
                    throw new Exception("AllowedClientVersionRange is empty!");
                }

                Version clientVersion = GetClientVersionHeader(request) ?? DefaultClientMinVersion;//请求方未声明，则为1.0.0
                if (clientVersion == null)
                {
                    clientVersion = DefaultClientMinVersion;
                }

                foreach (var range in AllowedClientVersionRangeList)
                {
                    if (range.MinVersion.CompareTo(clientVersion) <= 0 && range.MaxVersion.CompareTo(clientVersion) >= 0)
                    {
                        return true;
                    }
                }

                return false;
            }

            return false;
        }

        private Version GetClientVersionHeader(HttpRequestMessage request)
        {
            string clientVersionAsString;
            IEnumerable<string> headerValue;
            if (request.Headers.TryGetValues(VersionConsts.ClientVersionHeaderName, out headerValue) && headerValue.Count() == 1)
            {
                clientVersionAsString = headerValue.First();
            }
            else
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(clientVersionAsString))
            {
                return null;
            }

            return new Version(clientVersionAsString);
        }

        private SysCode? GetSysCodeHeader(HttpRequestMessage request)
        {
            string sysCodeAsString;
            IEnumerable<string> headerValue;
            if (request.Headers.TryGetValues(VersionConsts.SysCodeHeaderName, out headerValue) && headerValue.Count() == 1)
            {
                sysCodeAsString = headerValue.First();
            }
            else
            {
                return null;
            }

            SysCode tag;
            if (sysCodeAsString != null && Enum.TryParse<SysCode>(sysCodeAsString, out tag))
            {
                return tag;
            }

            return null;
        }

        private int? GetApiVersionHeader(HttpRequestMessage request)
        {
            string apiVersionAsString;
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(VersionConsts.VersionHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                apiVersionAsString = headerValues.First();
            }
            else
            {
                return null;
            }

            int version;
            if (apiVersionAsString != null && Int32.TryParse(apiVersionAsString, out version))
            {
                return version;
            }

            return null;
        }
    }
}