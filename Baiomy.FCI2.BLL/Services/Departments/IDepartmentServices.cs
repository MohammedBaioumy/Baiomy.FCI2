using Baiomy.FCI2.BLL.DTOs.Departments;
using Baiomy.FCI2.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.Services.Departments
{
    public interface IDepartmentServices
    {
        // in this interface we write signature for services[business]
        IEnumerable<DepartmentDTO>  GetAllDepartmetns();

        Task<DepartmentDetailsDTO?>  GetDepartmentAsync(int id);

        Task<int>  CreateDepartmentAsync(CreateDepartmentDTO department);

        Task<int>  UpdateDepartmentAsync(UpdateDepartmentDTO department);

        Task<bool> DeleteDepartmentAsync(int id);


    }
}
