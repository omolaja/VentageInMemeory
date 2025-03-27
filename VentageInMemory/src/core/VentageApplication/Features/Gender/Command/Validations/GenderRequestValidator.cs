using System;
using FluentValidation;

namespace VentageApplication.Features.Gender.Command.Validations
{
	public class GenderRequestValidator : AbstractValidator<GenderRequest>
    {
		public GenderRequestValidator()
		{
			RuleFor(request => request.GenderModel)
				.SetValidator(new GenderRequestValidation());
		}
	}
}

