using Baiomy.FCI2.DAL.Entities.Employees;
using Baiomy.FCI2.DAL.Persistence.Data;
using Baiomy.FCI2.DAL.Persistence.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Persistence.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(BaiomyFCI2DbContext dbContext):base(dbContext)
        {
            
        }


    }
}
