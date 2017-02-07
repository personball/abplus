using System.Threading.Tasks;
using System.Web.Http;
using Abplus.WebApiVersionRoute.RoutingConstraints;

namespace Abplus.WebApiVersionRoute.Sample.Controllers
{
    public class ValueController : ApiController
    {
        /// <summary>
        /// Access With HttpHeader "Abplus.ApiVersion:1"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byApiVersion", 1)]
        public async Task<string> byApiVersion1()
        {
            return "ApiVersion1";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.ApiVersion:2"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byApiVersion", 2)]
        public async Task<string> byApiVersion2()
        {
            return "ApiVersion2";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.SysCode:H5"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/bySysCode", 1, SysCode.H5)]
        public async Task<string> bySysCodeH5()
        {
            return "OnlySysCodeH5";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.SysCode:IPhone" or "Abplus.SysCode:Andriod"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/bySysCode", 1, SysCode.IPhone|SysCode.Andriod)]
        public async Task<string> bySysCodeNotH5()
        {
            return "SysCode.IPhone|SysCode.Andriod";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.ClientVersion:1.1.1" or "Abplus.ClientVersion:1.1.0" (with value less than 1.1.2)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byVersionRangeString", 1, SysCode.H5,"*-1.1.1")]
        public async Task<string> byVersionRangeStringLessThan112()
        {
            return "byVersionRangeStringLessThan112";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.ClientVersion:1.1.3" (with version between 1.1.2 and 1.9.0)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byVersionRangeString", 1, SysCode.H5, "1.1.2-1.9.0")]
        public async Task<string> byVersionRangeString112To190()
        {
            return "byVersionRangeString111To222";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.ClientVersion:1.9.1"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byVersionRangeString", 1, SysCode.H5, "1.9.1")]
        public async Task<string> byVersionRangeStringOnly191()
        {
            return "byVersionRangeStringOnly191";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.ClientVersion:1.9.2" or "Abplus.ClientVersion:1.9.3"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byVersionRangeString", 1, SysCode.H5, "1.9.2", "1.9.3")]
        public async Task<string> byVersionRangeString192And193()
        {
            return "byVersionRangeString192And193";
        }

        /// <summary>
        /// Access With HttpHeader "Abplus.ClientVersion:1.9.4"  (with version greater than 1.9.4)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byVersionRangeString", 1, SysCode.H5, "1.9.4-*")]
        public async Task<string> byVersionRangeStringGreaterThan193()
        {
            return "byVersionRangeStringGreaterThan193";
        }

        /// <summary>
        /// Access With HttpHeaders
        /// "Abplus.ClientVersion:1.9.4"
        /// "Abplus.SysCode:IPhone"
        /// "Abplus.ApiVersion:2"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VersionedRoute("api/value/byVersionRangeString", 2, SysCode.IPhone, "1.9.4")]
        public async Task<string> byApiSysCodeVersionRange()
        {
            return "ApiVersion2-IPhone-194";
        }
    }
}
