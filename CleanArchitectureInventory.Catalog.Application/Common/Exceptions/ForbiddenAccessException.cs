using System;

namespace CleanArchitectureInventory.Catalog.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() 
        {
        }
    }
}

