using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Note.EntityLayer.Concrete;
using Note.Web.Helper;
using Note.Web.ViewModels;

namespace Note.Web.Controllers
{
    public class PasswordController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public PasswordController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
            if (appUser != null)
            {
                string passwordResetToken=await _userManager.GeneratePasswordResetTokenAsync(appUser);
                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Password", new
                {
                    userId = appUser.Id,
                    token = passwordResetToken,

                }, HttpContext.Request.Scheme);


                PasswordReset.PasswordResetSendEmail(passwordResetLink, forgotPasswordVM.Email,appUser.UserName);

                ViewBag.status = "E-mailinize şifre yenileme linki gönderilmiştir.";
            }
            else
            {
                ModelState.AddModelError("", "Sistemde Kayıtlı E-Posta Adresi Bulunamadı");
            }

            return View(forgotPasswordVM);
        }
    }
}
