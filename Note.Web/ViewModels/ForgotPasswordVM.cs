using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Note.Web.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [EmailAddress]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

    }
}
