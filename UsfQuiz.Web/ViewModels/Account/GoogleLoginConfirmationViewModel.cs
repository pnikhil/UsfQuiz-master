namespace UsfQuiz.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class GoogleLoginConfirmationViewModel
    {
        [Required]
        public string Username { get; set; }
    }
}
