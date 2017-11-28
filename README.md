![abplus_icon](https://github.com/personball/abplus/blob/master/abplus_icon.png?raw=true)
# abplus
Abp plus, an extension for Abp Framework. 

## Packages

|Package|Status|
|:------|:-----:|
|Abplus|[![NuGet version](https://badge.fury.io/nu/Abplus.svg)](https://badge.fury.io/nu/Abplus)|
|Abplus.EntityFramework|[![NuGet version](https://badge.fury.io/nu/Abplus.EntityFramework.svg)](https://badge.fury.io/nu/Abplus.EntityFramework)|
|Abplus.MqMessages|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.svg)](https://badge.fury.io/nu/Abplus.MqMessages)|
|Abplus.MqMessages.AuditingStore|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.AuditingStore.svg)](https://badge.fury.io/nu/Abplus.MqMessages.AuditingStore)|
|Abplus.MqMessages.RebusCore|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RebusCore.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RebusCore)|
|Abplus.MqMessages.RebusRabbitMqConsumer|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqConsumer.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqConsumer)|
|Abplus.MqMessages.RebusRabbitMqPublisher|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqPublisher.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RebusRabbitMqPublisher)|
|Abplus.MqMessages.RedisStoreMessageTracker|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.RedisStoreMessageTracker.svg)](https://badge.fury.io/nu/Abplus.MqMessages.RedisStoreMessageTracker)|
|Abplus.Web.Api|[![NuGet version](https://badge.fury.io/nu/Abplus.Web.Api.svg)](https://badge.fury.io/nu/Abplus.Web.Api)|
|Abplus.Web.SignalR|[![NuGet version](https://badge.fury.io/nu/Abplus.Web.SignalR.svg)](https://badge.fury.io/nu/Abplus.Web.SignalR)|
|Abplus.T4.PermissionsFromJson|[![NuGet version](https://badge.fury.io/nu/Abplus.T4.PermissionsFromJson.svg)](https://badge.fury.io/nu/Abplus.T4.PermissionsFromJson)|
|Abplus.MqMessages.IndexToES|[![NuGet version](https://badge.fury.io/nu/Abplus.MqMessages.IndexToES.svg)](https://badge.fury.io/nu/Abplus.MqMessages.IndexToES)|
## Remarks

|Package|Remark|
|:------|:-----:|
|Abplus|基础概念扩展及接口定义|
|Abplus.EntityFramework|EF辅助方法：预热，自动注册FluentApi配置类|
|Abplus.MqMessages|集成消息队列的扩展方案|
|Abplus.MqMessages.AuditingStore|集成消息队列的扩展方案，审计日志发送消息队列|
|Abplus.MqMessages.RebusCore|集成消息队列的扩展方案，Rebus Publisher的实现|
|Abplus.MqMessages.RebusRabbitMqConsumer|集成消息队列的扩展方案，消费端模块（具备发布消息能力）,使用方法参考Samples/Sample.RebusRabbitMqConsumer|
|Abplus.MqMessages.RebusRabbitMqPublisher|集成消息队列的扩展方案，生产端模块，使用方法参考Samples/Sample.RebusRabbitMqPublisher|
|Abplus.MqMessages.RedisStoreMessageTracker|集成消息队列的扩展方案，消费端消费行为的幂等支持|
|Abplus.Web.Api|WebApi基于请求头的版本化机制，使用方法见Samples/Abplus.WebApiVersionRoute.Sample。|
|Abplus.Web.SignalR|基于Redis的OnlineClientManager，解决Abp自带的[OnlineClientManager](https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/RealTime/OnlineClientManager.cs#L26)在线状态不跨进程共享的问题。|
|Abplus.T4.PermissionsFromJson|提供T4工具，自动从Json文件定义中生成权限定义和权限树|
|Abplus.MqMessages.IndexToES|提供一个泛型版RebusHandler及T4工具，自动替在MqMessagesT4Register注册的MqMessages通过T4生成代码，将消息索引到ElasticSearch。|


## License

[MIT](LICENSE)

![Which_LICENSE](https://github.com/personball/abplus/blob/master/Which_LICENSE.jpg?raw=true)