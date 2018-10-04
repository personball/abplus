using System;

namespace Sample.DotNetCorePublisherHost
{
    class Program
    {
        static void Main()
        {
            //As Topshelf not support dotnet core yet, just simple run below code for a sample.
            var bs = new SamplePublisherHostBootstrap();
            bs.Start();

            Console.ReadLine();
        }
    }
}
