using Demo.BLL.Interfaces;
using Demo.DAL;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly MVCAppDbContext _dbContext; //

        public GenericRepository(MVCAppDbContext dbContext) //Ask From ClR Create
                                                            //Object from DbContext 
        {
            //dbContext = /*new MVCAppDbContext();*/

            _dbContext = dbContext;
        }

        //Just(Add, Update, Delete) Will change state of object
        public void Add(T item)
         => _dbContext.Set<T>().Add(item); //for Example just state will change and is added 

        public void Update(T item)
          => _dbContext.Set<T>().Update(item);//Modified

        public void Delete(T item)
          => _dbContext.Set<T>().Remove(item); //Deleted

        public async Task<T> Get(int id)

            => await _dbContext.FindAsync<T>(id);
       

        //After Install package (microsoft.entityframeworkcore.proxies), We make  Eager Loading to load Data of Departments at Employee
        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return  (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            else
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }



    }
}
