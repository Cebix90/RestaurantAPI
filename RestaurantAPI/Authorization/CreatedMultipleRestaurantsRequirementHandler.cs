﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Authorization;

public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    private readonly RestaurantDbContext _context;

    public CreatedMultipleRestaurantsRequirementHandler(RestaurantDbContext context)
    {
        _context = context;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var userdId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        
        var createdRestaurantsCount = _context
            .Restaurants
            .Count(r => r.CreatedById == userdId);
        
        if(createdRestaurantsCount >= requirement.MinimumRestaurantsCreated)
            context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}