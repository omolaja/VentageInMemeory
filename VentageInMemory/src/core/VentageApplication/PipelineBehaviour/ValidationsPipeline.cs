using FluentValidation;
using MediatR;
using VentageApplication.StandardResponse;

public class ValidationsPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationsPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(result => result.Errors).Where(f => f != null).DistinctBy(f => f.ErrorMessage).ToList();

            if (failures.Count > 0)
            {
                var errorMessages = failures.Select(f => f.ErrorMessage).ToList();

                if (typeof(TResponse) == typeof(Response))
                {
                    object response = ResponseHelper.CreateResponse(ResponseStatus.ValidationError, errorMessages);
                    return (TResponse)response;
                }
            }
        }

        return await next(); 
    }
}
