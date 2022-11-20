using Microsoft.AspNetCore.Identity;
using Note.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.IdentityCustomValidator
{
    public class CustomUserValidator:IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isDigit = int.TryParse(user.UserName[0]!.ToString(), out _);

            if (isDigit)
            {
                errors.Add(new() { Code = "UserNameContainsFirtLetterDigit", Description = "Kullanıcı adı ilk harfi sayısal karakter olamaz" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);

        }

    }
}
