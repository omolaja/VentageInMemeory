using System;
using FluentValidation;
using VentageApplication.Models;

namespace VentageApplication.Features.Customer.Command.Validations
{
	public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
		public CustomerRequestValidator()
		{
			RuleFor(request => request.CustomerModel)
				.SetValidator(new CustomerRequestValidation());
		}

    }
}

