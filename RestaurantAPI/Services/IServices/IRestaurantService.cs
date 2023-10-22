using RestaurantAPI.Models;

namespace RestaurantAPI.Services.IServices;

public interface IRestaurantService
{
    bool Delete(int id);
    int Create(CreateRestaurantDto dto);
    RestaurantDto GetById(int id);
    List<RestaurantDto> GetAll();
    bool Update(int id, UpdateRestaurantDto dto);
}