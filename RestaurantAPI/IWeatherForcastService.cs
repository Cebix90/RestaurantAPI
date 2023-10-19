namespace RestaurantAPI;

public interface IWeatherForcastService
{
    IEnumerable<WeatherForecast> Get();
    
    IEnumerable<WeatherForecast> Get2(int count, int minTemperature, int maxTemperature);
}