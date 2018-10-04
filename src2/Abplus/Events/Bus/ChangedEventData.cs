namespace Abp.Events.Bus
{
    public abstract class ChangedEventData<T> : EventData
    {
        public T Origin { get; set; }
        public T Current { get; set; }
    }
}
