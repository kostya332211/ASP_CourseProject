using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Chess.Web.Models.ValidationModels
{
    public class LoginValidationModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Please, enter Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please, enter Password")]
        [MinLength(8, ErrorMessage = "Min length of password 8 characters")]
        public string Password { get; set; }
    }
}
