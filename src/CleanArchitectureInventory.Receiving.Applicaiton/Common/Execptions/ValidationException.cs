using System;
using FluentValidation.Results;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Execptions
{
    public class ValidationException :Exception
    {
        public Dictionary<string,string[]> Errors { get; set; }
        public ValidationException():base("One or more validation excepiton occured.")
        {
            Errors = new Dictionary<string, string[]>();

        }
        public ValidationException(IEnumerable<ValidationFailure> failures)
            :base()
        {
            Errors = failures.GroupBy(t => t.PropertyName, t => t.ErrorMessage)
                .ToDictionary(t => t.Key, t => t.ToArray());
        }



    }
}

