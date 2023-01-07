using System.ComponentModel.DataAnnotations;

namespace Shoper.UI.Models.ViewModels
{
    public class registerViewModel
    {
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

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
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }


    }
}
