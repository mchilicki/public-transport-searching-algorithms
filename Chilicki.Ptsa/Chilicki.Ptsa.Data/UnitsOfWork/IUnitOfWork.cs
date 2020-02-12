using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.UnitsOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Dispose();
    }
}
