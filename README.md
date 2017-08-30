![abplus_icon](https://github.com/personball/abplus/blob/master/abplus_icon.png?raw=true)
# abplus
Abp plus, an extension for Abp Framework. 

## 组件功能介绍

### Abplus.Web.Api：
包含一套WebApi版本化方案，使用方法见Samples/Abplus.WebApiVersionRoute.Sample。

### Abplus.Web.SignalR：  
包含一个RedisOnlineClientManager，解决Abp自带的[OnlineClientManager](https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/RealTime/OnlineClientManager.cs#L26)在线状态不跨进程共享的问题。

### Abplus.Web.SimpleCaptcha：  
一个简单图片验证码模块。

### Abplus.MqMessages.RebusRabbitMqPublisher 
队列消息发布模块,使用方法参考Samples/Sample.RebusRabbitMqPublisher

### Abplus.MqMessages.RebusRabbitMqConsumer 
队列消息消费模块,使用方法参考Samples/Sample.RebusRabbitMqConsumer

### Abplus.MqMessages.AuditingStore
审计日志发消息队列

## License

MIT
