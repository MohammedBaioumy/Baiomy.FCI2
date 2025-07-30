using AutoMapper;
using Baiomy.FCI2.BLL.DTOs.Departments;
using Baiomy.FCI2.BLL.DTOs.Employees;
using Baiomy.FCI2.PL.ViewModels;

namespace Baiomy.FCI2.PL.Mapping
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            #region Employee
            CreateMap<EmployeeViewModel, CreatedEmployeeDTO>();
            CreateMap<EmployeeDetailsDTO, EmployeeViewModel>();
            CreateMap<EmployeeViewModel,UpdatedEmployeeDTO >();

            #endregion

            #region Department
            CreateMap<DepartmentViewModel,CreateDepartmentDTO>();
            CreateMap<DepartmentViewModel, UpdateDepartmentDTO>();

            #endregion
        }
    }
}
