using System;
using CleanArchitectureInventory.Catalog.Application.Common.Models;

namespace CleanArchitectureInventory.Catalog.Application.Common.Abstractions
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<Result> DeleteUserAsync(string userId);
    }
}

