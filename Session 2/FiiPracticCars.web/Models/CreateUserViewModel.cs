using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FiiPracticCars.web.Models
{
    public class CreateUserViewModel
    {
        [Microsoft.Build.Framework.Required]
        [MaxLength(50, ErrorMessage ="The first name is invalid")]
        //public int Id { get; set; }

        public string FirstName { get; set; }
        [Microsoft.Build.Framework.Required]
        [MaxLength(50, ErrorMessage = "The last name is invalid")]
        public string LastName { get; set; }

        [Microsoft.Build.Framework.Required]
        [EmailAddress(ErrorMessage = "The email addres is invalid")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //public DateTime? BirthDate { get; set; }
    }
}
