using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IAccountService
{
    Task RegisterUserAsync(RegisterUserDto dto);
    string GenerateJwt(LoginDto dto);
}