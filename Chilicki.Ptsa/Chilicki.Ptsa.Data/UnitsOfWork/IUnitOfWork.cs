using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.UnitsOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Dispose();
    }
}
