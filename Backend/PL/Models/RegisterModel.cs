using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}