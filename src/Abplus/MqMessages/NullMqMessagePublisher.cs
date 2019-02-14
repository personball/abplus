using System.Threading.Tasks;

namespace Abp.MqMessages
{
    /// <summary>
    /// 空模式
    /// </summary>
    public class NullMqMessagePublisher : IMqMessagePublisher
    {
        /// <summary>
        /// 
        /// </summary>
        public static NullMqMessagePublisher Instance { get; } = new NullMqMessagePublisher();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mqMessages"></param>
        /// <returns></returns>
        public Task PublishAsync(object mqMessages)
        {
            //do nothing.
            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mqMessages"></param>
        public void Publish(object mqMessages)
        {
            //do nothing.
        }
    }
}
