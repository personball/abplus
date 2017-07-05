using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.MqMessages.MessageTrackers
{
    public class DefaultInMemoryMessageTracker : IMessageTracker
    {
        private static readonly ConcurrentBag<string> InMemoryStore;

        static DefaultInMemoryMessageTracker()
        {
            InMemoryStore = new ConcurrentBag<string>();
        }

        public static DefaultInMemoryMessageTracker Instance { get { return SingletonInstance; } }
        private static readonly DefaultInMemoryMessageTracker SingletonInstance = new DefaultInMemoryMessageTracker();

        public Task MarkAsProcessed(string processId)
        {
            InMemoryStore.Add(processId);
            return Task.FromResult(0);
        }

        public Task<bool> HasProcessed(string processId)
        {
            return Task.FromResult(InMemoryStore.Contains(processId));
        }
    }
}
