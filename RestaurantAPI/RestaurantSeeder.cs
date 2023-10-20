﻿using RestaurantAPI.Entities;

namespace RestaurantAPI;

public class RestaurantSeeder
{
    private readonly RestaurantDbContext _dbContext;

    public RestaurantSeeder(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                _dbContext.SaveChanges();
            }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>
        {
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes = new List<Dish>
                {
                    new()
                    {
                        Name = "Nashville Hot Chicken",
                        Price = 10.30M
                    },

                    new()
                    {
                        Name = "Chicken Nuggets",
                        Price = 5.30M
                    }
                },
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                }
            },
            new()
            {
                Name = "McDonald Szewska",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                }
            }
        };

        return restaurants;
    }
}