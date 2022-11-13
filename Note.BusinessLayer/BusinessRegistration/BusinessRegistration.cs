using Microsoft.Extensions.DependencyInjection;
using Note.BusinessLayer.Abstract;
using Note.BusinessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.BusinessRegistration
{
    public static class BusinessRegistration
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {
            services.AddScoped<IAppNoteService, AppNoteManager>();
        }
    }
}
