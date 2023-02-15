using System;
namespace CleanArchitectureInventory.Catalog.Application.Common.Security
{
    /// <summary>
    ///  class this applied requires authorization
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true,Inherited = true)]
    public class AuthorizationAttribute :Attribute
    {
        
        public AuthorizationAttribute()
        {

        }

        public string Roles { get; set; } = string.Empty;
        public string Policy { get; set; } = string.Empty;
    }
}

