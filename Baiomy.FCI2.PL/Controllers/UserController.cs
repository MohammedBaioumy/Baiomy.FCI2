using Baiomy.FCI2.DAL.Entities.Identity;
using Baiomy.FCI2.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Baiomy.FCI2.PL.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #region Index (ALL USERS)
        public IActionResult Index()
        {
            var users = _userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email ?? "Not Exist",
                UserName = u.UserName ?? "Not Exist"
            });

            return View(users);
        }
        #endregion


        #region User Details
        public async Task<IActionResult> Details(string id , string? ViewName="Details")
        {
            if (id is null)
                return NotFound();

            var user =await _userManager.Users.Where(u => u.Id == id).Select(u => new DetailsUpdateUserViewModel()
            {
                Id= id,
                Email= u.Email ??"",
                UserName= u.UserName ??"",
                FirstName = u.FirstName ,
                LastName= u.LastName
            }).FirstOrDefaultAsync();
             return View(ViewName,user);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            if (id is null)
                return  NotFound();

            return await Details(id,"Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id,DetailsUpdateUserViewModel _model)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

                try
                {
                    if (user is not null)
                    {
                        user.Email = _model.Email;
                        user.FirstName = _model.FirstName;
                        user.LastName = _model.LastName;
                        user.UserName = _model.UserName;


                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            TempData["Operation"] = "Employee was updated :) ";
                            TempData["ToastColor"] = "text-dark-emphasis bg-info";
                            return RedirectToAction(nameof(Index));
                        }

                        TempData["Operation"] = "Sorry failed to update the employee :( ";
                        TempData["ToastColor"] = "text-light bg-danger";
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);                    
                }
            }

            return View(_model);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(string id)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    TempData["Operation"] = "Sorry failed to delete the employee :( ";
                    TempData["ToastColor"] = "text-light bg-danger";
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                TempData["Operation"] = "Employee Deleted successfully";
                TempData["ToastColor"] = "text-dark-emphasis bg-info";
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        #endregion

       
    }
}
