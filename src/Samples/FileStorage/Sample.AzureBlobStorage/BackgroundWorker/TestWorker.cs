using System;
using System.IO;
using Abp.Dependency;
using Abp.IO;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;

namespace Sample.AzureBlobStorage.BackgroundWorker
{
    public class TestWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IFileStorage _fileStorage;

        public TestWorker(AbpTimer timer, IFileStorage fileStorage)
            : base(timer)
        {
            _fileStorage = fileStorage;

            Timer.Period = 300 * 1000;//300 seconds
            Timer.RunOnStart = true;
        }

        protected override void DoWork()
        {
            var file = File.ReadAllBytes("plane.jpg");
            var newName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.jpg";
            var url = AsyncHelper.RunSync(() => _fileStorage.Save(file, newName, "test"));
            Console.WriteLine(url);

            var bytesFromAzure = AsyncHelper.RunSync(() => _fileStorage.ReadAsBytes(newName, "test"));
            File.WriteAllBytes(newName, bytesFromAzure);

            AsyncHelper.RunSync(() => _fileStorage.Delete(newName, "test"));

            using (var fs = new FileStream("plane.jpg", FileMode.Open))
            {
                var sName = $"{newName}-as-stream.jpg";
                var url2 = AsyncHelper.RunSync(() => _fileStorage.Save(fs, sName, "test"));
            }
        }
    }
}
