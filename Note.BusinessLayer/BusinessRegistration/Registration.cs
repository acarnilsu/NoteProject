using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Note.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.DataAccessLayer.Concrete;
using Note.BusinessLayer.IdentityCustomValidator;
using Microsoft.Extensions.Options;

namespace Note.BusinessLayer.BusinessRegistration
{
    public static class Registration
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {



            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.AllowedUserNameCharacters =
            "abcçdefghiıjklmnoöpqrstuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSTUÜVWXYZ0123456789-_";
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;


                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 5;



            })

                .AddPasswordValidator<CustomPasswordValidator>()
                .AddUserValidator<CustomUserValidator>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();




            services.Configure<DataProtectionTokenProviderOptions>(options =>
        options.TokenLifespan = TimeSpan.FromMinutes(1));
        }

    }
}
