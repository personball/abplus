using Abp.Auditing.AuditingStore;
using Abp.Configuration.Startup;
using Abp.Json;
using Abp.Modules;
using Abp.MqMessages.Consumers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sample.AuditingConsumerHandler
{
    [DependsOn(
        typeof(RebusRabbitMqConsumerModule)
        , typeof(AuditingConsumerRebusHandlerModule))]
    public class SampleAuditingConsumerHandlerModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqConsumer()
                .UseLogging(c => c.ColoredConsole())
                .ConnectTo("amqp://dev:dev@rabbitmq.local.abplus.cn/")//set your own connection string of rabbitmq
                .UseQueue(Assembly.GetExecutingAssembly().GetName().Name)//TODO@personball 需要开放更多队列选项，比如审计日志队列对可靠性要求不用很高，通过禁用队列持久化以提升消息发布速度
                .Prefetch(100)//用于控制每次拉取的资源消耗(内存,带宽),消费速度还要看消费端自己的消息处理速度
                .RegisterHandlerInAssemblys(Assembly.GetAssembly(typeof(AuditingConsumerRebusHandlerModule)));//注册AuditingConsumerRebusHandler

            Configuration.Modules.AuditingConsumer()
                .EveryPeriodIn(TimeSpan.FromMilliseconds(100))//每隔多久调用一次Do
                .Batch(100)//请实际测试审计消息的产生速度和消费速度，适当调节Prefetch、EveryPeriodIn以及Batch，以免消费端发生消息堆积导致内存溢出
                .Do(async (messageList) =>
                {
                    var lines = new List<string>();
                    foreach (var message in messageList)
                    {
                        lines.Add(message.ToJsonString());
                    }
                    await File.AppendAllLinesAsync(AppDomain.CurrentDomain.BaseDirectory + "\\messages.txt", lines);
                });

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void PostInitialize()
        {
            //Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));
        }
    }
}
