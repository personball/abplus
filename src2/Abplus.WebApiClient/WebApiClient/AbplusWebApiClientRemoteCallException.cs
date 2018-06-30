using System;
using System.Runtime.Serialization;

namespace Abp.WebApiClient
{
    /// <summary>
    /// 
    /// </summary>
    public class AbplusWebApiClientRemoteCallException : AbpException
    {
        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        public AbplusWebApiClientRemoteCallException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        public AbplusWebApiClientRemoteCallException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message"></param>
        public AbplusWebApiClientRemoteCallException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public AbplusWebApiClientRemoteCallException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}
