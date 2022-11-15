using System.ComponentModel.DataAnnotations;

namespace Note.Web.ViewModels
{
    public class SignInVM
    {
        [Required(ErrorMessage ="Bu alan boş geçilemez")] 
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Password { get; set; }

    }
}
