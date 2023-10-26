using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IAccountService
{
    void RegisterUserAsync(RegisterUserDto dto);
    string GenerateJwt(LoginDto dto);
}