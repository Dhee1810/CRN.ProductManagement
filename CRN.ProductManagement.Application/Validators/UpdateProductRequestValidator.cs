using CRN.ProductManagement.Application.DTOs;
using FluentValidation;

namespace CRN.ProductManagement.Application.Validators;

public class UpdateProductRequestValidator
    : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(255);
    }
}