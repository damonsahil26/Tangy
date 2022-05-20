using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models.DTO
{
    public class SignUpRequestDTO
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Enter valid email address")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password doesn't match Idiot!!")]
        public string ConfirmPassword { get; set; }
    }
}
