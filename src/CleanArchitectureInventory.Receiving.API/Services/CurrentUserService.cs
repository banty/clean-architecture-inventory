using System;
using System.Security.Claims;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureInventory.Receiving.API.Services
{
    public class CurrentUserService:ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

