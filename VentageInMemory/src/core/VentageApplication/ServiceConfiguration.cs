using System;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace VentageApplication
{
	public static class ServiceConfiguration
	{
		public static IServiceCollection ApplicationSerives(this IServiceCollection services)
		{
			return services.AddAutoMapper(Assembly.GetExecutingAssembly())
				.AddMediatR(Assembly.GetExecutingAssembly())
				.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}

