using System.ComponentModel.DataAnnotations;
using System;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel:ModelBase

    {
     

        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
        [MinLength(4, ErrorMessage = "Min Length is 4 Chars")]
        public string Name { get; set; }

        [Range(22, 30, ErrorMessage = "Age Must be betweeen 22 and 30")]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
        , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Range(4000, 8000)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; } //Time that Employee is Hired at it 

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        //[ForeignKey("Department")]
        public int? DepartmentId { get; set; } //F.K 
        //[InverseProperty(nameof(Models.Department.Employees))]
        //Navigational propery => [ONE] [Related Data]
        public Department Department { get; set; }
    }
}
 