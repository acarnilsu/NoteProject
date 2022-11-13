using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        IQueryable<T> TGetAll();
        Task<T> TGetByIdAsync(string id);
        Task TAddAsync(T entity);
        void TRemove(T entity);
        void TUpdate(T entity);

    }
}
