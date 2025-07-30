using AutoMapper;
using Baiomy.FCI2.BLL.Common.AttatchmentService;
using Baiomy.FCI2.BLL.DTOs.Employees;
using Baiomy.FCI2.BLL.Services.Departments;
using Baiomy.FCI2.BLL.Services.Employees;
using Baiomy.FCI2.DAL.Common.Enums;
using Baiomy.FCI2.DAL.Entities.Employees;
using Baiomy.FCI2.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Baiomy.FCI2.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {


        #region Dependacy Injection
        private readonly IEmployeeServices _employeeServices;
        private readonly IHostEnvironment _webhostEnvironment;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeServices employeeServices,
                                  IWebHostEnvironment webhostEnvironment,
                                  ILogger<EmployeeController> logger,
                                  IMapper mapper
                                 )
        {
            _employeeServices = employeeServices;
            _webhostEnvironment = webhostEnvironment;
            _logger = logger;
            _mapper = mapper;
           
        }
        #endregion


        #region Index
        public IActionResult Index()
        {
            var employees = _employeeServices.GetAllEmployeesAsQuarable();
            return View(employees);
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
        public async Task<IActionResult> CreateAsync(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
                return View(employeeViewModel);
            var message = string.Empty;
            try
            {
                /// var createdEmployeeDTO = new CreatedEmployeeDTO()
                /// {
                /// Address = employeeViewModel.Address,
                /// Age = employeeViewModel.Age,
                /// Email = employeeViewModel.Email,
                /// Gender = (employeeViewModel.Gender),
                /// HirringDate = employeeViewModel.HirringDate,
                /// EmployeeType = (employeeViewModel.EmployeeType),
                /// IsActive = employeeViewModel.IsActive,
                /// Name = employeeViewModel.Name,
                /// Salary = employeeViewModel.Salary,
                /// PhoneNumber = employeeViewModel.PhoneNumber,
                /// DepartmentID = employeeViewModel.DepartmentID,
                /// Image= employeeViewModel.Image,
                /// };
                


                var createdEmployeeDTO =_mapper.Map<CreatedEmployeeDTO>(employeeViewModel);
                var employee =await _employeeServices.AddEmployeeAsync(createdEmployeeDTO);
                if (employee > 0)
                {
                    TempData["Operation"] = "Employee Added successfully :) ";
                    TempData["ToastColor"] = "text-dark-emphasis bg-info";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Sorry an error occured during adding new employee :( ";
                }
            }
            catch (Exception ex)
            {
                message = _webhostEnvironment.IsDevelopment() ? ex.Message : "Sorry an error occured during adding new employee :(";
                _logger.LogError(ex, message);
            }

            ModelState.AddModelError("a", message);
            TempData["Operation"] = message; 
            TempData["ToastColor"] = "text-light bg-danger";

            return View(employeeViewModel);
        }

        #endregion


        #region Details

        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id is not null)
            {
                var employee =await _employeeServices.GetEmployeeAsync(id.Value);

                if (employee is { })
                    return View(employee);
                return NotFound();
            }
            return BadRequest();
        }

        #endregion


        #region Update

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int? id)
        {
            if (id is not null)
            {
                var updatedEmp =await _employeeServices.GetEmployeeAsync(id.Value);
                if (updatedEmp is not null)
                {
                    return View(new EmployeeViewModel()
                    {
                        Address = updatedEmp.Address,
                        Age = updatedEmp.Age,
                        Email = updatedEmp.Email,
                        Gender = (Gender)Enum.Parse(typeof(Gender), updatedEmp.Gender),
                        HirringDate = updatedEmp.HirringDate,
                        EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), updatedEmp.EmployeeType),
                        IsActive = updatedEmp.IsActive,
                        Name = updatedEmp.Name,
                        Salary = updatedEmp.Salary,
                        PhoneNumber = updatedEmp.PhoneNumber,
                        DepartmentID = updatedEmp.DepartmentID,
                        Image = updatedEmp.ImageFF
                    });


                    //var updatedDepVM = _mapper.Map<EmployeeViewModel>(updatedEmp);
                    //return View(updatedDepVM);

                }
                return NotFound();
            }
            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
                return View(employeeViewModel);
            var message = string.Empty;
            try
            {
                /// var updatedEmployeeDTO = new UpdatedEmployeeDTO()
                /// {
                ///     Id = id,
                ///     Address = employeeViewModel.Address,
                ///     Age = employeeViewModel.Age,
                ///     Email = employeeViewModel.Email,
                ///     Gender = (employeeViewModel.Gender),
                ///     HirringDate = employeeViewModel.HirringDate,
                ///     EmployeeType = (employeeViewModel.EmployeeType),
                ///     IsActive = employeeViewModel.IsActive,
                ///     Name = employeeViewModel.Name,
                ///     Salary = employeeViewModel.Salary,
                ///     PhoneNumber = employeeViewModel.PhoneNumber,
                ///     DepartmentID=employeeViewModel.DepartmentID,
                /// };
                /// 

                var updatedEmployeeDTO = _mapper.Map<UpdatedEmployeeDTO>(employeeViewModel);
                updatedEmployeeDTO.Id= id;
                var updatedEmployee =await _employeeServices.UpdateEmployeeAsync(updatedEmployeeDTO);
                if (updatedEmployee > 0)
                {
                    TempData["Operation"] = "Employee was updated :) ";
                    TempData["ToastColor"] = "text-dark-emphasis bg-info";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Sorry an error occured during updating :(";
                }
            }
            catch (Exception ex)
            {
                message = _webhostEnvironment.IsDevelopment() ? ex.Message : "Sorry an error occured during updating new employee :(";
                _logger.LogError(ex, message);
            }


            ModelState.AddModelError("a", message);
            TempData["Operation"] = "Sorry failed to update the employee :( ";
            TempData["ToastColor"] = "text-light bg-danger";           
            return View(employeeViewModel);
        }


        #endregion


        #region Delete
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteAsync(int id)
        {


            var message = string.Empty;
            try
            {
                var result =await _employeeServices.DeleteEmployeeAsync(id);
                if (result)
                {
                    TempData["Operation"] = "Employee Deleted successfully";
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

                message = _webhostEnvironment.IsDevelopment() ? ex.Message : "An error occured during deletion :( ";
                _logger.LogError(ex.Message, ex);

            }
            ModelState.AddModelError("c", message);
            TempData["Operation"] = "Sorry failed to delete the employee :( ";
            TempData["ToastColor"] = "text-light bg-danger";

            return RedirectToAction(nameof(Index));

        }



        #endregion



        #region ViewData VS ViewBag and TempData

        // ViewData and ViewBag
        // both used to transfere data from action to view [PartialView - Layout] and
        // both using and can access the same view storage
        // ViewData is a Dictionary Type property introduced in .NetFramework 3.5 
        // ViewData Enforce type safety by detecting the data type of the value at compilation time 
        // ViewData need casting  
        // ViewData is faster than ViewBag
        //
        //                    ****************
        // ViewBag is a dynamic type property introduced in .NetFramework 4.0
        // viewBag can't Enforce type safety because CLR detecting the data type of the valie at RunTime
        // ViewBag doesn't need casting
        // ViewBag is slower than ViewData
        //                  __*****************__ 
        // 
        //  TempData is a Dictionary Type property introduced in .NetFramework 3.5
        //  TempData used to transfere data between two actions or requsets
        //  TempData only works during the current and subsequent request [request and subrequest]
        // 

        #endregion



    }
}
