using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IRestaurantService
{
    RestaurantDto GetById(int id);
    List<RestaurantDto> GetAll();
    int Create (CreateRestaurantDto dto);
}