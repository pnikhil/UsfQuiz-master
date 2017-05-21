namespace UsfQuiz.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using UsfQuiz.Data.Commons;

    public class LoginViewModel
    {
        [Required]
        [MinLength(GlobalConstants.UsernameMinLength, ErrorMessage = GlobalConstants.MinimumLength)]
        [MaxLength(GlobalConstants.UsernameMaxLength, ErrorMessage = GlobalConstants.MaximumLength)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
