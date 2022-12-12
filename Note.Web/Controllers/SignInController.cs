using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Note.EntityLayer.Concrete;
using Note.Web.Helper;
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
                    Email = signInVM.Email,
                    UserName=signInVM.UserName

                };
                var result = await _userManager.CreateAsync(user, signInVM.Password);

                if (result.Succeeded)
                {
                    string ConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user); //usera göre bir token veriyor

                    string link = Url.Action("ConfirmEmail", "SignIn", new  //1.method adı, 2.controller adı, 3. linkin devamı
                    {
                        userId = user.Id, //yukarıda oluşturduğum userin idsi
                        token = ConfirmToken,

                    }, protocol: HttpContext.Request.Scheme
                    );

                    EmailConfirmation.EmailConfirmSendEmail(link, signInVM.Email, signInVM.UserName);



                    return RedirectToAction("SendEmail");
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


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.Status = "E-posta adresiniz onaylanmıştır";
            }
            else
            {
                ViewBag.Status = "Bir hata oluştu. Lütfen daha sonra tekrar deneyiniz";
            }

            return View();

        }


        public IActionResult SendEmail()
        {
            return View();
        }

    }
}
