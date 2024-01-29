using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    //signature for Property for each Repository
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }

        public IDepartmentRepository DepartmentRepository { get; set; }

        //signature for Method => (Complete) that represent  SaveChanges at DBContext
        //return int => Number of rows that had affect ,
       Task< int> Complete();


    }
}
