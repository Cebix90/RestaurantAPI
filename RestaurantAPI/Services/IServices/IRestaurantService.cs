using System.Security.Claims;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services.IServices;

public interface IRestaurantService
{
    void Delete(int id);
    int Create(CreateRestaurantDto dto);
    RestaurantDto GetById(int id);
    List<RestaurantDto> GetAll();
    void Update(int id, UpdateRestaurantDto dto);
}