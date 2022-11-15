using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Note.EntityLayer.Concrete;
using Note.Web.ViewModels;

namespace Note.Web.Controllers
{
    public class SignInController : Controller
    {
        private readonly UserManager<AppUser> _userManager; //UserManager default olarak geliyor BL de oluşturmaya gerek yok.
        private readonly IMapper _mapper;

        public SignInController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM signInVM)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Email = signInVM.Email

                };
                var result = await _userManager.CreateAsync(user, signInVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }

            }
            return View(signInVM);
        }
    }
}
