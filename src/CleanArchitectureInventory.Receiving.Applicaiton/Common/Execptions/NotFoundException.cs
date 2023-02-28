using System;
namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Execptions
{
    public class NotFoundException :Exception
    {
        public NotFoundException():base()
        {

        }
        public NotFoundException(string message):base(message)
        {

        }

        public NotFoundException(string message, Exception innerException):base(message,innerException)
        {
            
        }
        public NotFoundException(string name, string value)
            :base($"Entity {name} with key {value} not found")
        {

        }

        

        
    }
}

