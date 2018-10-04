using System;

namespace Sample.DotNetCoreConsumerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var bs = new SampleConsumerHostBootstrap();
            bs.Start();

            Console.ReadLine();
        }
    }
}
