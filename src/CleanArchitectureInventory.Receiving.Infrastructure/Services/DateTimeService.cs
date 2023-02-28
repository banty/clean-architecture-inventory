using System;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;

namespace CleanArchitectureInventory.Receiving.Infrastructure.Services
{
    public class DateTimeService :IDateTime
    {
        public DateTimeService()
        {
        }

        public DateTime GetDateTime => DateTime.Now;
    }
}

