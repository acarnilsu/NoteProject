using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Note.Web.ViewModels
{
    public class LogInVM
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [EmailAddress]
        [DisplayName("E-Mail")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Şifre")]
        public string Password { get; set; }


    }
}
