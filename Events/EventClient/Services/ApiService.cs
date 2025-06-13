using EventClient.DTOs;
using System.Text.Json;
using System.Text;

namespace EventClient.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetConnectionString("ApiUrl") ?? "https://localhost:7001/api";
        }

        private void SetAuthHeader(string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<UserDto>> GetClientsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/users/clients");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<UserDto>();
                }
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error getting clients: {ex.Message}");
            }
            return new List<UserDto>();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserDto>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user: {ex.Message}");
            }
            return null;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(loginDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/auth/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AuthResponseDto>(responseJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
            }
            return null;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(registerDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/auth/register", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(EditUserDto userDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(userDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/users/{userDto.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/users/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return false;
            }
        }
    }
}
