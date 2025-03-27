using System;
using FluentValidation;
using VentageApplication.Models;

namespace VentageApplication.Features.Customer.Command.Validations
{
	public class CustomerUpdateRequestValidations : AbstractValidator<CustomerUpdateModel>
    {
        public CustomerUpdateRequestValidations()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Customer name is required.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater 0");
            RuleFor(x => x.Website).Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri,
                UriKind.Absolute, out _)).WithMessage("Invalid website URL format.");
        }
	}
}

