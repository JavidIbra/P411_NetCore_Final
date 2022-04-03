using System.ComponentModel.DataAnnotations;

namespace CodeAcademyApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(100)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool KeepMeLoggedIn { get; set; }
    }
}
