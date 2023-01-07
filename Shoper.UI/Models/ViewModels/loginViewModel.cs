using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Shoper.UI.Models.ViewModels
{
    public class loginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }

    }
}
