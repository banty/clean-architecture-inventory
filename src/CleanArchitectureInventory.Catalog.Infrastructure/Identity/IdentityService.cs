using System;
using AutoMapper;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Identity
{
    public class IdentityService :IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _userClaimPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;

        }


        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser()
            {
                UserName = userName,
                Email = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u=>u.Id==userId);

            return user !=null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u=>u.Id==userId);

            if (user == null) return false;
            var principal = await _userClaimPrincipalFactory.CreateAsync(user);
            var authorize = await _authorizationService.AuthorizeAsync(principal, policyName);
            return authorize.Succeeded;
        }

       

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }


        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.ToApplicationResult();
        }


    }
}

