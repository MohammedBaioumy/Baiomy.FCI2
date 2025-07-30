using Baiomy.FCI2.BLL.DTOs.Employees;


namespace Baiomy.FCI2.BLL.Services.Employees
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeesDTO> GetAllEmployeesAsQuarable();

        Task<EmployeeDetailsDTO?> GetEmployeeAsync(int id);

        Task<int> AddEmployeeAsync(CreatedEmployeeDTO createdEmployeeDTO);

        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDTO updatedEmployeeDTO);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}
