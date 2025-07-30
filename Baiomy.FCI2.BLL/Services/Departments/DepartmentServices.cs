using Baiomy.FCI2.BLL.DTOs.Departments;
using Baiomy.FCI2.DAL.Entities.Departments;
using Baiomy.FCI2.DAL.Persistence.Repositories.Departments;
using Baiomy.FCI2.DAL.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.Services.Departments
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        #region Using Repository
        //private readonly IDepartmentRepository _departmentRepository;

        //public DepartmentServices(IDepartmentRepository departmentRepository)
        //{
        //    _departmentRepository = departmentRepository;
        //}

        #endregion

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDTO> GetAllDepartmetns()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAllAsQueryable().Where(d=>!d.IsDeleted).Select(d => new DepartmentDTO()
            {
                Id = d.ID,
                Code = d.Code,
                CreationDate = d.CreationDate,
                Description = d.Description,
                Name = d.Name
            });
            return departments;

        }

        public async Task<DepartmentDetailsDTO?> GetDepartmentAsync(int id)
        {
            var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department is { })
                return new DepartmentDetailsDTO()
                {
                    ID=department.ID,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                    CreatedBy = 1,
                    Name = department.Name,
                    CreatedOn = department.CreatedOn,
                    Description = department.Description,
                    LastModificationBy = 1,
                    LastModificationOn = DateTime.UtcNow
                };
            return null;
        }
        public async Task<int> CreateDepartmentAsync(CreateDepartmentDTO departmentDto)
        {
            var department = new Department()
            {
                Code = departmentDto.Code,
                CreatedBy = 1,
                Name = departmentDto.Name,
                CreatedOn = DateTime.UtcNow,
                Description = departmentDto.Description,
                CreationDate= departmentDto.CreationDate,

            };
             _unitOfWork.DepartmentRepository.Add(department);
            return await _unitOfWork.CompleteAsync();
        }


        public async Task<int> UpdateDepartmentAsync(UpdateDepartmentDTO departmentDto)
        {
            var department = new Department()
            {
                ID = departmentDto.ID,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
                CreationDate=departmentDto.CreationDate,
            };
             _unitOfWork.DepartmentRepository.Update(department);
            return await _unitOfWork.CompleteAsync();

        }
        public async Task<bool>  DeleteDepartmentAsync(int id)
        {
                var UnfDepRep = _unitOfWork.DepartmentRepository;
            var department =await UnfDepRep.GetByIdAsync(id);
            if (department is { })
            {
                UnfDepRep.Delete(department);
                return await _unitOfWork.CompleteAsync() > 0;
            }

            return false;
        }

       
    }
}
