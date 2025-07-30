using Baiomy.FCI2.DAL.Entities;
using Baiomy.FCI2.DAL.Entities.Departments;
using Baiomy.FCI2.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Persistence.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {


        private readonly BaiomyFCI2DbContext _dbContext;

        public GenericRepository(BaiomyFCI2DbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<IEnumerable<T>> GetAllAsync(bool AsNoTracing = true)
        {
            if (AsNoTracing)
            {
                
                var items = await _dbContext.Set<T>().AsNoTracking().ToListAsync();
                return items;
            }
            return await _dbContext.Set<T>().AsTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Add(T T)
           =>  _dbContext.Set<T>().Add(T);
           
        
        public void Update(T T)
           =>  _dbContext.Set<T>().Update(T);
            
        

        public void Delete(T T)
        {
            // Applying soft delete 
            T.IsDeleted = true;
            _dbContext.Set<T>().Update(T);           
        }

        public  IQueryable<T> GetAllAsQueryable(bool AsNoTracing = true)
        {

            {
                if (AsNoTracing)
                {
                    var items = _dbContext.Set<T>().AsNoTracking();
                    return items;
                }
                return _dbContext.Set<T>();
            }
        }
    } 
}

