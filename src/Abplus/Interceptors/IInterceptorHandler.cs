using System;
using System.Threading.Tasks;

namespace Abp.Interceptors
{
    public interface IInterceptorHandler
    {
        void Handle(Action action);
        Task Handle(Func<Task> action);
        Task<T> Handle<T>(Func<Task<T>> action);
    }
}
