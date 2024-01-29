using AutoMapper;
using Demo.DAL;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        //when take object from this class , I will teach it how to convert fromEmployeeViewModel to Employee ,
        //Through CreateMap< > that inherited from Profile
        public MappingProfiles()
        {
        //With take value that at property for example Name that at EmployeeViewModel and put it at property Name that at Employee
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();



            CreateMap<DepartmentViewModel, Department>().ReverseMap();

        }
    }
}
