using ProPri.Comum.Dominio;
using System;

namespace ProPri.Comum.Dados
{
    public interface IRepository<T> : IDisposable where T : IRaizAgregacao
    {
        IUnitOfWork UnitOfWork { get; }
    }
}