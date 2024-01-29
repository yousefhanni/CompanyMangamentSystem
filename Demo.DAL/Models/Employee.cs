using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    //Model=> class Represent shape data that exist inside table 
    public class Employee:ModelBase
    {
        public string Name { get; set; }
         public int? Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; } 
        public bool IsActive { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; } //Time that Employee is Hired at it 

        public bool ISDeleted { get; set; } = false; //Soft Delete
        public DateTime CreationDate { get; set; }= DateTime.Now; //Time that Record is created at Database

        public string ImageName { get; set; }
        public int? DepartmentId { get; set; } //F.K 
        //Navigational propery => [ONE] [Related Data]
        public Department Department { get; set; }

    }
}
