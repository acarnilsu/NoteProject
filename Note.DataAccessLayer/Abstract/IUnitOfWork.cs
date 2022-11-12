using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.DataAccessLayer.Abstract
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Save();
    }
}
