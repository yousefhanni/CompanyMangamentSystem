using Demo.BLL.Interfaces;
using Demo.DAL;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MVCAppDbContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<Employee> GetEmployeeByaddress(string address)
        => _dbContext.Employees.Where(E => E.Address.ToLower().Contains(address.ToLower()));

        //Searching 
        public IQueryable<Employee> SearchByName(string name)
       => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));  //search => Compare Two Names 
    }
}
