using Demo.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Demo.DAL.Models;

namespace Demo.PL.ViewModels
{
    public class DepartmentViewModel: ModelBase
    {

        [Required(ErrorMessage = "Code is required!!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required!!")]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        //[InverseProperty(nameof(Employee.Department))]
        //Navigational propery => [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
