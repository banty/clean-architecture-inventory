using System;
using FluentValidation.Results;

namespace CleanArchitectureInventory.Catalog.Application.Common.Exceptions
{
    public class ValidationException:Exception
    {
        public ValidationException()
            :base("One or more validation failure have occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> faliures):this()
        {
            Errors = faliures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                            .ToDictionary(faliureGroup => faliureGroup.Key, faliureGroup => faliureGroup.ToArray());

        }

        public IDictionary<string, string[]> Errors { get; set; }


    }
}

