using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Contexts;
using Demo.BLL.Repositories;

namespace Demo.BLL
{
    //UnitOfWork: Unit for each The Work With DbContext 
    //UnitOfWork Encapsulate DbContext ,PL deal with UnitOfWork  and UnitOfWork deal with DbContext 
    //UnitOfWork => [Represent(deal with) Repositories + Implement Complete Method that  Save Changes at DbContext + Dispose Method that dispsose connection]
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly MVCAppDbContext _dbContext;

        //Automatic properties => Compiler will get and set for Repository at Background
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }


        //CTOR, Because when create object from UnitOfWork=> Make Initialization to two Repositories
        public UnitOfWork(MVCAppDbContext dbContext) //ASk CLR for creating Object DbContext
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
        }
        //Add Dbcontext Save Changes 
        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }


        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
