using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IRestaurantService
{
    bool Delete(int id);
    int Create(CreateRestaurantDto dto);
    RestaurantDto GetById(int id);
    List<RestaurantDto> GetAll();
}