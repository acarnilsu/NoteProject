using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Note.EntityLayer.Concrete;
using Note.Web.Areas.Manager.ViewModels;

namespace Note.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Roles()
        {
            var roles = await _roleManager.Roles.OrderBy(x => x.Name).ToListAsync();
            return View(_mapper.Map<List<CreateRoleVM>>(roles));

        }


        [HttpGet]
        public IActionResult CreateRoles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles(CreateRoleVM createRoleVM)
        {
            AppRole appRole = new();

            //if (ModelState.IsValid)
            
                appRole.Name= createRoleVM.Name;

                var result=await _roleManager.CreateAsync(appRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            
            return View(createRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> RoleDelete(string id)
        {
            AppRole appRole=await _roleManager.FindByIdAsync(id);
            if (appRole != null)
            {
                var result=await _roleManager.DeleteAsync(appRole);

            }
            return RedirectToAction("Roles");
        }


        [HttpGet]
        public async Task<IActionResult> RoleUpdate(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            if(appRole != null)
            {
                return View(_mapper.Map<CreateRoleVM>(appRole));
            }
            return RedirectToAction("Roles");
        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(CreateRoleVM createRoleVM)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(createRoleVM.Id);
            if (appRole != null)
            {
                appRole.Name = createRoleVM.Name;
                var result=await _roleManager.UpdateAsync(appRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Güncelleme Başarısız Oldu");
            }
            return View(createRoleVM);


        }
    }
}
