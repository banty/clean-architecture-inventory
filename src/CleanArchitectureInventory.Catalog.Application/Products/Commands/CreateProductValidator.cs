using System;
using FluentValidation;

namespace CleanArchitectureInventory.Catalog.Application.Products.Commands
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}

