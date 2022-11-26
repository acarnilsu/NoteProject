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



        [HttpGet]
        public IActionResult ResetPasswordConfirm(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(NewPasswordVM viewModel)
        {

            if (viewModel.UserId == null)
            {
                viewModel.UserId = TempData["userId"].ToString();
                viewModel.Token = TempData["token"].ToString();
            }

            if (ModelState.IsValid)
            {

                AppUser user = await _userManager.FindByIdAsync(viewModel.UserId);

                if (user != null)
                {

                    var result = await _userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);

                        TempData["PasswordResetInfo"] = "Şifreniz başarı bir şekilde yenilenmiştir.";

                        await _signInManager.SignOutAsync();

                        return RedirectToAction("LogIn", "LogIn");

                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(item.Code, item.Description);
                        }
                    }
                }

                else
                {

                    ModelState.AddModelError("", "Hata meydana geldi. Tekrar deneyiniz");


                    return View(viewModel);
                }

            }

            return View(viewModel);
        }

        

    }
}
