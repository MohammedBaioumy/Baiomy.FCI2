using Baiomy.FCI2.BLL.Common.AttatchmentService;
using Baiomy.FCI2.BLL.DTOs.Employees;
using Baiomy.FCI2.DAL.Common.Enums;
using Baiomy.FCI2.DAL.Entities.Departments;
using Baiomy.FCI2.DAL.Entities.Employees;
using Baiomy.FCI2.DAL.Persistence.Repositories.Employees;
using Baiomy.FCI2.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttatchmentService _attatchmentService;
        #region UsingRepository
        //private readonly IEmployeeRepository _employeeRepositry;

        //public EmployeeServices(IEmployeeRepository employeeRepository)
        //{
        //    _employeeRepositry = employeeRepository;
        //} 
        #endregion

        public EmployeeServices(IUnitOfWork unitOfWork , IAttatchmentService attatchmentService)
        {
            _unitOfWork = unitOfWork;
            _attatchmentService = attatchmentService;
        }
        public IEnumerable<EmployeesDTO> GetAllEmployeesAsQuarable()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAllAsQueryable().Where(e => !e.IsDeleted).Include(e => e.Department).Select(e => new EmployeesDTO()
            {
                Id = e.ID,
                Name = e.Name,
                Age = e.Age,
                Email = e.Email,
                IsActive = e.IsActive,
                Salary = e.Salary,
                EmployeeType = e.EmployeeType.ToString(),
                Gender = e.Gender.ToString(),
                Department = e.Department == null ? "---" : e.Department.Name,
                Image = e.Image
            });  
            return employees;
        }

        public async Task<EmployeeDetailsDTO?>  GetEmployeeAsync(int id)
        {

            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee is { } && !employee.IsDeleted) { 
                var employeeDetailsDTO = new EmployeeDetailsDTO()
                {
                    Id = employee.ID,
                    Address = employee.Address,
                    Age = employee.Age,
                    Email = employee.Email,
                    Gender = employee.Gender.ToString(),
                    HirringDate = employee.HirringDate,
                    EmployeeType = (employee.EmployeeType).ToString(),
                    IsActive = employee.IsActive,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    PhoneNumber = employee.PhoneNumber,
                    CreatedOn = employee.CreatedOn,
                    CreatedBy = 1,
                    LastModificationBy = 1,
                    LastModificationOn = DateTime.UtcNow,
                    Department = employee.Department == null ? "  ---  " : employee.Department.Name,
                    DepartmentID = employee.DepartmentID,
                    Image = employee.Image
                };
                
            return employeeDetailsDTO;
            }


                return null;
            
        }

        public async Task<int> AddEmployeeAsync(CreatedEmployeeDTO createdEmployeeDTO)
        {
            var employee = new Employee()
            {
                Address = createdEmployeeDTO.Address,
                Age = createdEmployeeDTO.Age,
                Email = createdEmployeeDTO.Email,
                Gender =  (createdEmployeeDTO.Gender), 
                HirringDate = createdEmployeeDTO.HirringDate,
                EmployeeType = (createdEmployeeDTO.EmployeeType),
                IsActive = createdEmployeeDTO.IsActive,
                Name = createdEmployeeDTO.Name,
                Salary = createdEmployeeDTO.Salary,
                PhoneNumber = createdEmployeeDTO.PhoneNumber,
                CreatedBy=1,   
                DepartmentID= createdEmployeeDTO.DepartmentID,
                
            };
            if(createdEmployeeDTO.Image is  not null)
            employee.Image =await _attatchmentService.UploadFileAsync(createdEmployeeDTO.Image, "Images");
             _unitOfWork.EmployeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            var employee = new Employee()
            {
                ID=updatedEmployeeDTO.Id,
                Address = updatedEmployeeDTO.Address,
                Age = updatedEmployeeDTO.Age,
                Email = updatedEmployeeDTO.Email,
                Gender = (updatedEmployeeDTO.Gender),
                HirringDate = updatedEmployeeDTO.HirringDate,
                EmployeeType =( updatedEmployeeDTO.EmployeeType),
                IsActive = updatedEmployeeDTO.IsActive,
                Name = updatedEmployeeDTO.Name,
                Salary = updatedEmployeeDTO.Salary,
                PhoneNumber = updatedEmployeeDTO.PhoneNumber,   
                DepartmentID= updatedEmployeeDTO.DepartmentID
            };
            if (updatedEmployeeDTO.Image is not null)
                employee.Image =await _attatchmentService.UploadFileAsync(updatedEmployeeDTO.Image, "Images");
            _unitOfWork.EmployeeRepository.Update(employee);
            return await _unitOfWork.CompleteAsync();

        }

        public async Task<bool>  DeleteEmployeeAsync(int id)
        {
            // we will apply soft delete here 
            var UnfEmpRep = _unitOfWork.EmployeeRepository;
            var employee =await UnfEmpRep.GetByIdAsync(id);
            if(employee is { })
                if (!employee.IsDeleted)
                {
                    employee.IsDeleted = true;
                    UnfEmpRep.Update(employee);
                    return await _unitOfWork.CompleteAsync()>0;
                }
            return false;
        }


    }
}
