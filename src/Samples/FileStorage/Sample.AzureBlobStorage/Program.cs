using System;

namespace Sample.AzureBlobStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            //As Topshelf not support dotnet core yet, just simple run below code for a sample.
            var bs = new SampleAzureBlobStorageBootstrap();
            bs.Start();

            Console.ReadLine();
        }
    }
}
