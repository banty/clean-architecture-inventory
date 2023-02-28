using System;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime GetDateTime() => DateTime.Now;
    }
}

