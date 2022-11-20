using Microsoft.AspNetCore.Identity;
using Note.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Note.BusinessLayer.IdentityCustomValidator
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                if (!user.Email.Contains(user.UserName))
                {
                    errors.Add(new IdentityError() { Code = "PasswordContainsUserName", Description = "şifre alanı kullanıcı adı içeremez" });
                }
            }

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {

                errors.Add(new IdentityError() { Code = "PasswordContainsUserName", Description = "şifre alanı kullanıcı adı içeremez" });

            }
            Regex regex = new Regex("([a-z0-9])\\1\\1");

            if (regex.Match(password.ToLower()).Success)
            {
                errors.Add(new IdentityError() { Code = "PasswordContain1234", Description = "Şifre Alanı Ardışık Aynı Sayı Veya Harf  İçeremez" });
            }

            if (password.ToLower().Contains(user.Email.ToLower()))
            {
                errors.Add(new IdentityError() { Code = "PasswordContainsEmail", Description = "şifre alanı email adresiniz içeremez" });
            }

            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));

            }
        }
    }
}
