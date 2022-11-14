using Note.BusinessLayer.Abstract;
using Note.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericDal<T> _genericDal;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericDal<T> genericDal, IUnitOfWork unitOfWork)
        {
            _genericDal = genericDal;
            _unitOfWork = unitOfWork;
        }

        public async Task TAddAsync(T entity)
        {
            await _genericDal.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public IQueryable<T> TGetAll()
        {
            return _genericDal.GetAll();
        }

        public async Task<T> TGetByIdAsync(string id)
        {
            return await _genericDal.GetByIdAsync(id);
        }

        public void TRemove(T entity)
        {
            _genericDal.Remove(entity);
            _unitOfWork.Save();
        }

        public void TUpdate(T entity)
        {
            _genericDal.Update(entity);
            _unitOfWork.Save();
        }
    }
}
