using System;

namespace Abp.MqMessages.AutoGeneration
{
    /// <summary>
    /// 标记指定EventData不参与自动生成MqMessage
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DontGenerateMqMessage : Attribute
    {
    }
}
