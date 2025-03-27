using System;
using FluentValidation;
using VentageApplication.Models;

namespace VentageApplication.Features.Gender.Command.Validations
{
	public class GenderRequestValidation : AbstractValidator<GenderModel>
    {
		public GenderRequestValidation()
		{
            RuleFor(x => x.Name).NotEmpty().WithMessage("Gender Name/Type is required.");
        }
	}
}

