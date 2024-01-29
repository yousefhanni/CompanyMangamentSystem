using Demo.DAL;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        //I use IQueryable not  IEnumerable because I need make Filteration
        IQueryable<Employee> GetEmployeeByaddress(string address);

        IQueryable<Employee> SearchByName(string name);
    }
}
