using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Chess.Web.Models.ValidationModels
{
    public class RegisterValidationModel
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please, enter First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please, enter Last Name")]
        public string LastName { get; set; }

        [DisplayName("Nickname")]
        [Required(ErrorMessage = "Please, come up with a nickname")]
        public string Nickname { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please, enter Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please, enter Password")]
        [MinLength(8, ErrorMessage = "Min length of password 8 characters")]
        public string Password { get; set; }

        [DisplayName("Repeat Password")]
        [Required(ErrorMessage = "Please, repeat Password")]
        [MinLength(8, ErrorMessage = "Min length of password 8 characters")]
        public string RePassword { get; set; }
    }
}
