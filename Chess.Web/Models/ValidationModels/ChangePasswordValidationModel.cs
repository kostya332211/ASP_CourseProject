using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Chess.Web.Models.ValidationModels
{
    public class ChangePasswordValidationModel
    {
        [DisplayName("New Password")]
        [Required(ErrorMessage = "Please, enter new Password")]
        [MinLength(8, ErrorMessage = "Min length of password 8 characters")]
        public string Password { get; set; }

        [DisplayName("Repeat New Password")]
        [Required(ErrorMessage = "Please, repeat Password")]
        [MinLength(8, ErrorMessage = "Min length of password 8 characters")]
        public string RePassword { get; set; }
    }
}
