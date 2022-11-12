using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);

    }
}
