﻿using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using RestaurantAPI.Services.IServices;

namespace RestaurantAPI.Services;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<RestaurantService> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextService _userContextService;

    public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger, 
        IAuthorizationService authorizationService, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _authorizationService = authorizationService;
        _userContextService = userContextService;
    }

    public void Delete(int id)
    {
        _logger.LogError($"Restaurant with id: {id} DELETE action invoked");
        
        var restaurant = _dbContext
            .Restaurants
            .FirstOrDefault(r => r.Id == id);
        
        if (restaurant is null) throw new NotFoundException("Restaurant not found");
        
        var authoizationResult = _authorizationService
            .AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
        if(!authoizationResult.Succeeded) throw new ForbidException();
        
        _dbContext.Restaurants.Remove(restaurant);
        _dbContext.SaveChanges();
    }
    
    public int Create(CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        restaurant.CreatedById = _userContextService.GetUserId;
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

        if (restaurant is null) throw new NotFoundException("Restaurant not found");

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
    
    public void Update(int id, UpdateRestaurantDto dto)
    {
        var restaurant = _dbContext
            .Restaurants
            .FirstOrDefault(r => r.Id == id);

        if (restaurant is null) throw new NotFoundException("Restaurant not found");

        var authoizationResult = _authorizationService
            .AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
        if(!authoizationResult.Succeeded) throw new ForbidException();
        
        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.HasDelivery = dto.HasDelivery;

        _dbContext.SaveChanges();
    }
}