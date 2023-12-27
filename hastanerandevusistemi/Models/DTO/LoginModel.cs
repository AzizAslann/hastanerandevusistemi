using System.ComponentModel.DataAnnotations;

namespace hastanerandevusistemi.Models.DTO
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Kullanıcı adı zorunludur!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Kullanıcı şifresi zorunludur!")]
        public string Password { get; set; }
    }
}
