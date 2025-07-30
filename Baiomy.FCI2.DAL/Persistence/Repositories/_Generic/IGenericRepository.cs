using Baiomy.FCI2.DAL.Entities;
using Baiomy.FCI2.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Persistence.Repositories._Generic
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAllAsync(bool AsNoTracing = true);

        IQueryable<T> GetAllAsQueryable(bool AsNoTracing = true);

        Task<T?> GetByIdAsync(int id);

        void Add(T item);

        void Update(T item);

        void Delete(T item);
    }
}
