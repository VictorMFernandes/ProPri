using System.Threading.Tasks;

namespace ProPri.Comum.Dados
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}