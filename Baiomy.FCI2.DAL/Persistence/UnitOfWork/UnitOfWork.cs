using Baiomy.FCI2.DAL.Persistence.Data;
using Baiomy.FCI2.DAL.Persistence.Repositories.Departments;
using Baiomy.FCI2.DAL.Persistence.Repositories.Employees;

namespace Baiomy.FCI2.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaiomyFCI2DbContext _dbContext;

        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_dbContext);

        public UnitOfWork(BaiomyFCI2DbContext dbContext)
        {
            _dbContext = dbContext;
           // EmployeeRepository = new EmployeeRepository(_dbContext);
           // DepartmentRepository = new DepartmentRepository(_dbContext);
        }
        public async ValueTask DisposeAsync()
        {
           await _dbContext.DisposeAsync();
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

      
    }
}
