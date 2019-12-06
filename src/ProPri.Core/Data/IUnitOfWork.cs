using System.Threading.Tasks;

namespace ProPri.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}