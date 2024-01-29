using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    //View of Register that will appears to you on the page
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5 ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required!")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password does not Match password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }


    }
}
