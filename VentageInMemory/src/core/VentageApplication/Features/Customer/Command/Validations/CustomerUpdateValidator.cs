using System;
using FluentValidation;

namespace VentageApplication.Features.Customer.Command.Validations
{
	public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateRequest>
	{
		public CustomerUpdateValidator()
		{
			RuleFor(request => request.CustomerUpdateModel).SetValidator(new CustomerUpdateRequestValidations());
		}
	}
}

