using EventClient.DTOs;

namespace EventClient.Services
{
    public interface IApiService
    {
        Task<List<UserDto>> GetClientsAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<bool> UpdateUserAsync(EditUserDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
