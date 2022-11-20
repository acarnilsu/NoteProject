using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Note.Web.ViewModels
{
    public class SignInVM
    {
        [Required(ErrorMessage ="Bu alan boş geçilemez")] 
        [EmailAddress]
        [DisplayName("E-Posta")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi tekrar girin")]
        [Compare("Password", ErrorMessage = "şifreler uyuşmuyor lütfen kontrol edin")]
        public string ConfirmPassword { get; set; }

    }
}
