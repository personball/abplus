![abplus_icon](https://github.com/personball/abplus/blob/master/abplus_icon.png?raw=true)
# abplus
Abp plus, an extension for Abp Framework. 

## 组件功能介绍
* Abplus.Events.Producer：提供直接发布事件到消息队列的一种抽象（不推荐直接使用，代码较简单，使用前请仔细评估）。
* Abplus.RebusRabbitmqProducer：提供针对Abplus.Events.Producer的基于Rebus的对接Rabbitmq的实现（不推荐直接使用，使用前请仔细评估）。
* Abplus.RebusRabbitmqConsumer：提供对应Abplus.RebusRabbitmqProducer的消费端实现（不推荐直接使用，使用前请仔细评估）。
* Abplus.Web.Api：包含一套WebApi版本化方案，使用方法见Samples/Abplus.WebApiVersionRoute.Sample。
* Abplus.Web.SignalR：包含一个RedisOnlineClientManager，解决Abp自带的[OnlineClientManager](https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/RealTime/OnlineClientManager.cs#L26)在线状态不跨进程共享的问题。
* Abplus.Web.SimpleCaptcha：一个简单图片验证码模块。

## License
MIT
