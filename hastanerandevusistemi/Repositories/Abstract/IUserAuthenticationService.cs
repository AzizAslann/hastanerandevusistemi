using hastanerandevusistemi.Models.DTO;

namespace hastanerandevusistemi.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegistrationAsync(RegistrationModel model);
        Task LogoutAsync();

        Task<string> GetLoggedInUserId();
    }
}
