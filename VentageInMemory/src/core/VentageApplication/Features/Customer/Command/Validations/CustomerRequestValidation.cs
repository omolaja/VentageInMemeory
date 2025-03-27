using System;
using FluentValidation;
using VentageApplication.Models;

namespace VentageApplication.Features.Customer.Command.Validations
{
    public class CustomerRequestValidation : AbstractValidator<CustomerModel>
    {
        public CustomerRequestValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lastname is required.");
            RuleFor(x => x.GenderId).NotEmpty().GreaterThan(0).WithMessage("A Valid Gender is required.");
            RuleFor(x => x.Website).Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Invalid website URL format.");

        }

    }
}

