using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Note.EntityLayer.Concrete;
using Note.Web.ViewModels;

namespace Note.Web.Controllers
{
    public class LogInController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LogInController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM logInVM)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser=await _userManager.FindByEmailAsync(logInVM.Email);
                if (appUser != null)
                {
                    if (await _userManager.IsLockedOutAsync(appUser))
                    {
                        ModelState.AddModelError("", "Hesabınız bir süreliğine kitlenmiştir. Lütfen daha sonra tekrar deneyiniz");
                        return View(logInVM);

                    }

                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(appUser, logInVM.Password,true,true);
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(appUser);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "E-Mail veya şifre yanlış");
                    }
                    
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(String.Empty, "5 kez E-Mail adresinizi veya Şifrenizi yanlış girdiğiniz için hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz");
                        return View(logInVM);

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "E-Mail veya şifre yanlış");
                    return View(logInVM);
                }

                ModelState.AddModelError("", $"E-Mail adresiniz veya Şifreniz yanlıştır. {await _userManager.GetAccessFailedCountAsync(appUser)}  kez başarısız işlem gerçekleşmiştir.");

            }
            return View(logInVM);
        }
    }
}
