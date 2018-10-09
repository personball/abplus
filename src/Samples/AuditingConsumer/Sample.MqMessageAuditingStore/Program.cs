using System;

namespace Sample.MqMessageAuditingStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var bs = new SampleMqMessageAuditingStoreBootstrap();
            bs.Start();

            Console.ReadLine();
        }
    }
}
