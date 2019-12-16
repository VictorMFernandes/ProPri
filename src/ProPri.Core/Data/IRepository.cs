using ProPri.Core.Domain;
using System;
using System.Threading.Tasks;

namespace ProPri.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task<bool> Commit();
    }
}