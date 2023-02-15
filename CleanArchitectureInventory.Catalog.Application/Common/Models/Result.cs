using System;
namespace CleanArchitectureInventory.Catalog.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }
        public static Result Faliure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}

