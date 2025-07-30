using Baiomy.FCI2.DAL.Entities.Departments;
using Baiomy.FCI2.DAL.Persistence.Data;
using Baiomy.FCI2.DAL.Persistence.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {     
        public DepartmentRepository(BaiomyFCI2DbContext dbContext):base(dbContext)
        {           
        }

       
    }
}
