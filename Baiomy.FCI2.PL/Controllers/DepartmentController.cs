using AutoMapper;
using Baiomy.FCI2.BLL.DTOs.Departments;
using Baiomy.FCI2.BLL.Services.Departments;
using Baiomy.FCI2.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Baiomy.FCI2.PL.Controllers
{
    [Authorize]
  
    public class DepartmentController : Controller
    {

        #region Debendancy Injection

        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentServices departmentServices,
                                    ILogger<DepartmentController> logger,
                                    IWebHostEnvironment webHostEnvironment,
                                    IMapper mapper)
        {
            _departmentServices = departmentServices;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        #endregion


        #region Index
        public IActionResult Index()
        {
            var departments = _departmentServices.GetAllDepartmetns();
            return View(departments);
        }
        #endregion


        #region Create
        [HttpGet]
        public IActionResult CreateAsync()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DepartmentViewModel CreatedDepartmentViewModel)
        {
            if (!ModelState.IsValid)
                return View(CreatedDepartmentViewModel);

            var message = string.Empty;

            try
            {
                /// var createdDepartmentDTO = new CreateDepartmentDTO()
                /// {
                ///
                ///     Code = CreatedDepartmentViewModel.Code,
                ///     Description = CreatedDepartmentViewModel.Description,
                ///     Name = CreatedDepartmentViewModel.Name,
                ///     CreationDate = CreatedDepartmentViewModel.CreationDate
                /// };

                var createdDepartmentDTO = _mapper.Map<CreateDepartmentDTO>(CreatedDepartmentViewModel);
               var result =await _departmentServices.CreateDepartmentAsync(createdDepartmentDTO);

                if (result > 0)
                {
                    TempData["Operation"] = "Department Added successfully :) ";
                    TempData["ToastColor"] = "text-dark-emphasis bg-info";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Sorry an error accured so department not created :( ";
                    return View(CreatedDepartmentViewModel);
                }
            }
            catch (Exception ex)
            {

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Sorry an error accured so department not created :(";
                _logger.LogError(ex, message);

            }
            TempData["Operation"] = message;
            TempData["ToastColor"] = "text-light bg-danger";

            ModelState.AddModelError("a", message);
            return View(CreatedDepartmentViewModel);


        }
        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int? id)
        {
            if (id is null) return BadRequest();

            var department =await _departmentServices.GetDepartmentAsync(id.Value);

            if (department is { })
            {
                var updateVM = new DepartmentViewModel()
                {
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate
                };
                return View(updateVM);
            }


            return View("Error"); // if dep is null will return the error view 'not implemented by me yet'
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, DepartmentViewModel updateDepartmentViewModel)
        {
            if (!ModelState.IsValid)
                return View(updateDepartmentViewModel);

            var message = string.Empty;

            try
            {
                /// var updateDepDTO = new UpdateDepartmentDTO()
                /// {
                ///     ID = id,
                ///     Code = updateDepartmentViewModel.Code,
                ///     Description = updateDepartmentViewModel.Description,
                ///     Name = updateDepartmentViewModel.Name,
                ///     CreationDate = updateDepartmentViewModel.CreationDate
                /// };

                var updateDepDTO = _mapper.Map<UpdateDepartmentDTO>(updateDepartmentViewModel);
                updateDepDTO.ID=id;
                var result =await _departmentServices.UpdateDepartmentAsync(updateDepDTO);
                if (result > 0)
                {
                    TempData["Operation"] = "Department was updated :) ";
                    TempData["ToastColor"] = "text-dark-emphasis bg-info";

                    //TempData["message"] = "Department updated successfully :) ";
                    //TempData["Class"] = "text-bg-primary";

                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    message = "Sorry an error acured so department not updated :( ";
                    return View(updateDepartmentViewModel);
                }

            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Sorry an error acured so department not updated :( "; ;
                _logger.LogError(ex, message)
                    ;
            }

            TempData["Operation"] = "Sorry failed to update the department :( ";
            TempData["ToastColor"] = "text-light bg-danger";

            ModelState.AddModelError("b", message);
            return View(updateDepartmentViewModel);

        }

        #endregion


        #region Details
        
        [HttpGet]
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id is not null)
            {
                var result = await _departmentServices.GetDepartmentAsync(id.Value);

                if (result is { })
                    return View(result);


            }
            return View("Error"); // retrun error page we should make view for this error page
        }

        #endregion


        #region Delete

        #region Another way if delete [old way] like details but i used modal way so not need for this Action now
        #region Delete 'First way by adding new view '
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id is not null)
        //    {
        //        var result = _departmentServices.GetDepartmet(id.Value);

        //        if (result is { })
        //            return View(result);


        //    }
        //    return View("Error"); // retrun error page we should make view for this error page
        //} 
        #endregion

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var message = string.Empty;
            try
            {
                var deleted =await _departmentServices.DeleteDepartmentAsync(id);
                if (deleted)
                   {
                    TempData["Operation"] = "Department Deleted successfully";
                    TempData["ToastColor"] = "text-dark-emphasis bg-info";
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    message = "An error occured during deleted :( ";
                }
            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : message;
                _logger.LogError(ex.Message, ex);

            }
            TempData["Operation"] = "Sorry failed to delete the employee :( ";
            TempData["ToastColor"] = "text-light bg-danger";

            ModelState.AddModelError("c", message);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
