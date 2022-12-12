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

            appRole.Name = createRoleVM.Name;

            var result = await _roleManager.CreateAsync(appRole);

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
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            if (appRole != null)
            {
                var result = await _roleManager.DeleteAsync(appRole);

            }
            return RedirectToAction("Roles");
        }


        [HttpGet]
        public async Task<IActionResult> RoleUpdate(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            if (appRole != null)
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
                var result = await _roleManager.UpdateAsync(appRole);
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
            }
            else
            {
                ModelState.AddModelError("", "Güncelleme Başarısız Oldu");
            }
            return View(createRoleVM);


        }


        public async Task<IActionResult> Users()
        {

            var user = await _userManager.Users.ToListAsync();

            return View(_mapper.Map<List<AdminUserListVM>>(user));
        }



        [HttpGet]
        public IActionResult RoleAssign(string id)
        {
            TempData["userId"] = id;

            AppUser user = _userManager.FindByIdAsync(id).Result;

            ViewBag.userName = user.UserName;

            IQueryable<AppRole> roles = _roleManager.Roles;

            List<string> userRoles = (List<string>)_userManager.GetRolesAsync(user).Result;

            List<RoleAssignVM> viewModel = new();

            foreach (var role in roles)
            {
                RoleAssignVM roleAssignVM = new();
                roleAssignVM.Id = role.Id;
                roleAssignVM.Name = role.Name;

                if (userRoles.Contains(role.Name))
                    roleAssignVM.Exist = true;

                else
                    roleAssignVM.Exist = false;

                viewModel.Add(roleAssignVM);

            }

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignVM> roleAssignVM)
        {
            AppUser user = await _userManager.FindByIdAsync(TempData["userId"].ToString());

            foreach (var item in roleAssignVM)
            {
                if (item.Exist)

                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("Users");
        }

    }
}
