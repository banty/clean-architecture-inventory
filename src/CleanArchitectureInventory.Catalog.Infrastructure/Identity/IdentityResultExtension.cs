using System;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Identity
{
    public static class IdentityResultExtension
    {
        public static Result ToApplicationResult(this IdentityResult identityResult)
        {
            return identityResult.Succeeded ? Result.Success()
                : Result.Faliure(identityResult.Errors.Select(t => t.Description));
        }
    }
}

