using System.ComponentModel.DataAnnotations;

namespace Shoper.Management.Models.ViewModels
{
    public class registerViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz bir Email")]
        [Display(Name = "Eposta")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Geçersiz Parola")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Geçersiz Parola")]
        [Display(Name = "Şifre Doğrulama")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }


    }
}
