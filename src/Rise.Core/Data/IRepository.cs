using System;
using System.Threading.Tasks;
using Rise.Core.Domain;

namespace Rise.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task<bool> Commit();
    }
}