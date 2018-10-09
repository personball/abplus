![abplus_icon](https://github.com/personball/abplus/blob/master/abplus_icon.png?raw=true)
# abplus
Abp plus, an extension for Abp Framework. 

*From 2.0.0, all components upgrade to netstandard2.0.*  
注明 (deprecated)的将不再支持，将针对ef core, aspnet core, signalr core提供扩展。
## Packages

|Package|Status|
|:------|:-----|
|Abplus|[![NuGet version](https://badge.fury.io/nu/Abplus.svg)](https://badge.fury.io/nu/Abplus)|
|Abplus.MqMessages|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.svg)](https://badge.fury.io/nu/Abplus.MqMessages)|
|Abplus.MqMessages.AuditingStore|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.AuditingStore.svg)](https://badge.fury.io/nu/Abplus.MqMessages.AuditingStore)|
|Abplus.MqMessages.AuditingConsumerHandler|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.AuditingConsumerHandler.svg)](https://badge.fury.io/nu/Abplus.MqMessages.AuditingConsumerHandler)|
|Abplus.MqMessages.RebusCore|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RebusCore.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RebusCore)|
|Abplus.MqMessages.RebusRabbitMqConsumer|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqConsumer.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqConsumer)|
|Abplus.MqMessages.RebusRabbitMqPublisher|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqPublisher.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqPublisher)|
|Abplus.MqMessages.RedisStoreMessageTracker|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RedisStoreMessageTracker.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RedisStoreMessageTracker)|
|Abplus.T4.PermissionsFromJson|[![NuGet version](https://badge.fury.io/nu/Abplus.T4.PermissionsFromJson.svg)](https://badge.fury.io/nu/Abplus.T4.PermissionsFromJson)|
|Abplus.MqMessages.IndexToES|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.IndexToES.svg)](https://badge.fury.io/nu/Abplus.MqMessages.IndexToES)|
|Abplus.AspNetCore.SignalR|[![NuGet version](https://badge.fury.io/nu/Abplus.AspNetCore.SignalR.svg)](https://badge.fury.io/nu/Abplus.AspNetCore.SignalR)|
|Abplus.EntityFramework (deprecated)|[![NuGet version](https://badge.fury.io/nu/Abplus.EntityFramework.svg)](https://badge.fury.io/nu/Abplus.EntityFramework)|
|Abplus.Web.Api (deprecated)|[![NuGet version](https://badge.fury.io/nu/Abplus.Web.Api.svg)](https://badge.fury.io/nu/Abplus.Web.Api)|
|Abplus.Web.SignalR (deprecated)|[![NuGet version](https://badge.fury.io/nu/Abplus.Web.SignalR.svg)](https://badge.fury.io/nu/Abplus.Web.SignalR)|

## Remarks

|Package|Remark|
|:------|:-----|
|Abplus|基础概念扩展及接口定义|
|Abplus.MqMessages|集成消息队列的扩展方案|
|Abplus.MqMessages.AuditingStore|集成消息队列的扩展方案，审计日志发送消息队列，使用方法参考 src/Samples/AuditingConsumer|
|Abplus.MqMessages.AuditingConsumerHandler|集成消息队列的扩展方案，审计日志队列消费端，使用方法参考 src/Samples/AuditingConsumer|
|Abplus.MqMessages.RebusCore|集成消息队列的扩展方案，Rebus Publisher的实现|
|Abplus.MqMessages.RebusRabbitMqConsumer|集成消息队列的扩展方案，消费端模块（具备发布消息能力）,使用方法参考src/Samples/Sample.DotNetCoreConsumerHost或Sample.DotNetFxConsumerHost|
|Abplus.MqMessages.RebusRabbitMqPublisher|集成消息队列的扩展方案，生产端模块，使用方法参考Samples/Sample.DotNetCorePublisherHost或Sample.DotNetFxPublisherHost|
|Abplus.MqMessages.RedisStoreMessageTracker|集成消息队列的扩展方案，消费端消费行为的幂等支持|
|Abplus.T4.PermissionsFromJson|提供T4工具，自动从Json文件定义中生成权限定义和权限树|
|Abplus.MqMessages.IndexToES|提供一个泛型版RebusHandler及T4工具，自动替在MqMessagesT4Register注册的MqMessages通过T4生成代码，将消息索引到ElasticSearch。|
|Abplus.AspNetCore.SignalR|基于Redis的OnlineClientManager，解决Abp自带的[OnlineClientManager](https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/RealTime/OnlineClientManager.cs#L26)在线状态不跨进程共享的问题。|
|Abplus.EntityFramework(deprecated)|EF辅助方法：预热，自动注册FluentApi配置类|
|Abplus.Web.Api(deprecated)|WebApi基于请求头的版本化机制，使用方法见Samples/Abplus.WebApiVersionRoute.Sample。|
|Abplus.Web.SignalR(deprecated)|基于Redis的OnlineClientManager，解决Abp自带的[OnlineClientManager](https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/RealTime/OnlineClientManager.cs#L26)在线状态不跨进程共享的问题。|

## License

[MIT](LICENSE)

