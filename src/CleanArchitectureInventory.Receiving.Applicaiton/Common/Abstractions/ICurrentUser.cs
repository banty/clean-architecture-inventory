using System;
namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions
{
    public interface ICurrentUser
    {
        string? Name { get; }
    }
}

