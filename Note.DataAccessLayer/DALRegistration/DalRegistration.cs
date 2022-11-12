using Microsoft.Extensions.DependencyInjection;
using Note.DataAccessLayer.Abstract;
using Note.DataAccessLayer.Concrete;
using Note.DataAccessLayer.EntityFramework;
using Note.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.DataAccessLayer.DALRegistration
{
    public static class DalRegistration
    {
        public static void AddDalRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddScoped<IAppNoteDal, EfAppNoteDal>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
