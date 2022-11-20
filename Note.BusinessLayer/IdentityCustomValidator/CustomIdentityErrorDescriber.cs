using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.IdentityCustomValidator
{
    public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "Invalid Username",
                Description = $"{userName} geçersizdir."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "Password Too Short",
                Description = $"Şifreniz {length} karakterden küçük olamaz."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "Duplicate Username",
                Description = $"Bu kullanıcı adı \'{userName}\' kullanılmaktadır.."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "Duplicate Email",
                Description = $"Bu e-posta \'{email}\' kullanılmaktadır.."
            };
        }


        public override IdentityError InvalidToken()
        {
            return new IdentityError()
            {
                Code = "Invalid Username",
                Description = $"Şifre yenileme süresi dolmuştur."
            };
        }

    }
}
