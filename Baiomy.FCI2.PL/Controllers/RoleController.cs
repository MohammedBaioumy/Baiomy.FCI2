using Baiomy.FCI2.DAL.Entities.Identity;
using Baiomy.FCI2.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Baiomy.FCI2.PL.Controllers
{
      
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {     
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        #region Index (ALL ROLES)
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name ?? ""
            }).ToListAsync();

            return View(roles);
        }
        #endregion


        #region Create 

        [HttpGet]
        public  IActionResult Create()
        {
            return  View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(IdentityRole  role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ///var NewRole = new IdentityRole()
                    ///{
                    ///    Name = roleViewModel.Name,
                    ///};

                    var result =await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                       return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }


                }
                catch (Exception ex)
                {
                        ModelState.AddModelError(string.Empty, ex.Message);
                }

                
            }

            return View(role);
        }



        #endregion

        #region Update


        [HttpGet]  
        public async Task<IActionResult> Update(string id)
        {
            if(id is not null) 
            { 
                var role =await _roleManager.FindByIdAsync(id);
                if (role is { })
                {
                    ViewData["RoleId"] = id;
                    return View(role);
                }
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Update(string id,IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
                var role =await _roleManager.FindByIdAsync(id);
                if(role is { })
                {
                    if(role.Name==identityRole.Name)
                    {
                        TempData["Operation"] = "No changes accured on the rule name :) ";
                        TempData["ToastColor"] = "text-dark-emphasis bg-info";
                        return RedirectToAction(nameof(Index));
                    }
                    role.Name  = identityRole.Name ;

                    var result =await _roleManager.UpdateAsync(role);
                    if (!result.Succeeded)
                    {
                        TempData["Operation"] = "Sorry failed to update the Role :( ";
                        TempData["ToastColor"] = "text-light bg-danger";
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    TempData["Operation"] = "Role was updated :) ";
                    TempData["ToastColor"] = "text-dark-emphasis bg-info";
                    return RedirectToAction(nameof(Index));

                }
                
            }
            return View(identityRole);
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(string id)
        {
            if (id is not null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is { })
                    return View(role);
            }
            return NotFound();
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(string id)
        {
            if (id is not null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is { })
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                    {
                        TempData["Operation"] = "Sorry failed to delete the Role :( ";
                        TempData["ToastColor"] = "text-light bg-danger";
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    TempData["Operation"] = "Role Deleted successfully";
                    TempData["ToastColor"] = "text-dark-emphasis bg-info";
                    return  RedirectToAction("Index");

                }
            }

            return NotFound();
        }

        #endregion


        #region USER IN ROLE

        [HttpGet]
        public async Task<IActionResult> UserInRole(string RoleId)
        {
            var role =await _roleManager.FindByIdAsync(RoleId);
            var users =await _userManager.Users.ToListAsync();
            var UsersInRoleList = new List<UserInRoleViewModel>();
            ViewData["RoleId"] = RoleId;
            for (int i = 0; i < users.Count; i++)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId = users[i].Id,
                    UserName = users[i].UserName!,
                };
                userInRole.IsInRole = await _userManager.IsInRoleAsync(users[i], role?.Name ?? "");

                UsersInRoleList.Add(userInRole);

            }

            return View(UsersInRoleList);
        }

        [HttpPost]
        public async Task<IActionResult> UserInRole(string RoleId, List<UserInRoleViewModel> model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(RoleId);
                for (int i = 0; i < model.Count; i++)
                {
                    var user = await _userManager.FindByIdAsync(model[i].UserId);
                    if (user != null)
                    {
                        if (model[i].IsInRole == true && !await _userManager.IsInRoleAsync(user!, role?.Name ?? ""))
                        {
                            await _userManager.AddToRoleAsync(user, role?.Name??"");
                        }
                        if (model[i].IsInRole == false && await _userManager.IsInRoleAsync(user!, role?.Name ?? ""))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role?.Name ?? "");
                        }
                    }

                }
                return RedirectToAction(nameof(Update),new { id=RoleId });
            }
            return BadRequest();
        }

        #endregion

    }
}
