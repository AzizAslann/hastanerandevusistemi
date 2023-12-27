using Microsoft.AspNetCore.Identity;

namespace hastanerandevusistemi.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
