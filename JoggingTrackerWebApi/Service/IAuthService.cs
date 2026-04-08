using JoggingTrackerWebApi.Dto;

namespace JoggingTrackerWebApi.Service
{
    public interface IAuthService
    {
        Task<string> RegisterAsync (RegisterDto dto);
        Task<string> LoginAsync (LoginDto dto);
    }
}
