﻿using System;
namespace CleanArchitectureInventory.Catalog.Application.Common.Abstractions
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}

