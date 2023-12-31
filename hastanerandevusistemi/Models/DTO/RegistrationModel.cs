using System.ComponentModel.DataAnnotations;

namespace hastanerandevusistemi.Models.DTO
{
    public class RegistrationModel
    {
        [Required(ErrorMessage ="Bu alan zorunludur!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur ve şifrenizde 1 büyük, 1 küçük harf, 1 sembol, 1 sayı ve en az 6 uzunluklu olmalıdır.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Şifreni minimum 6 uzunluklu, 1 büyük harf, 1 küçük harf, 1 özel karakter ve 1 sayı içermeli!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        public string Role { get; set; }

    }
}
