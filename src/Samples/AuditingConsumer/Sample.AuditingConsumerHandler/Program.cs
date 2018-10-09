using System;

namespace Sample.AuditingConsumerHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var bs = new SampleAuditingConsumerHandlerBootstrap();
            bs.Start();

            Console.ReadLine();
        }
    }
}
