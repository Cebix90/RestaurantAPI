﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services.IServices;

namespace RestaurantAPI.Services;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<RestaurantService> _logger;

    public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public bool Delete(int id)
    {
        _logger.LogError($"Restaurant with id: {id} DELETE action invoked");
        
        var restaurant = _dbContext
            .Restaurants
            .FirstOrDefault(r => r.Id == id);
        
        if (restaurant is null) return false;
        
        _dbContext.Restaurants.Remove(restaurant);
        _dbContext.SaveChanges();

        return true;
    }
    
    public int Create(CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        _dbContext.Restaurants.Add(restaurant);
        _dbContext.SaveChanges();

        return restaurant.Id;
    }
    
    public RestaurantDto GetById(int id)
    {
        var restaurant = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);

        if (restaurant is null) return null;

        var result = _mapper.Map<RestaurantDto>(restaurant);
        return result;
    }
    
    public List<RestaurantDto> GetAll()
    {
        var restaurants = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .ToList();

        var result = _mapper.Map<List<RestaurantDto>>(restaurants);
        return result;
    }
    
    public bool Update(int id, UpdateRestaurantDto dto)
    {
        var restaurant = _dbContext
            .Restaurants
            .FirstOrDefault(r => r.Id == id);

        if (restaurant is null) return false;

        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.HasDelivery = dto.HasDelivery;

        _dbContext.SaveChanges();

        return true;
    }
}