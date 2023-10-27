using System.Security.Claims;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services.IServices;

public interface IRestaurantService
{
    void Delete(int id, ClaimsPrincipal user);
    int Create(CreateRestaurantDto dto, int userId);
    RestaurantDto GetById(int id);
    List<RestaurantDto> GetAll();
    void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user);
}