using FluentValidation;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MultiPayloadHandling.Filters;

// https://benfoster.io/blog/minimal-api-validation-endpoint-filters/
public static class ValidationFilter
{
    private static ConcurrentDictionary<Type, Type> validatorTypes = new ConcurrentDictionary<Type, Type>();
    public static EndpointFilterDelegate ValidationFilterFactory(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        var validationDescriptors = GetValidators(context.MethodInfo);
        if (validationDescriptors.Any())
        {
            return invocationContext => Validate(validationDescriptors, invocationContext, next);
        }

        // pass-thru
        return invocationContext => next(invocationContext);
    }

    private static async ValueTask<object?> Validate(IEnumerable<ValidationDescriptor> validationDescriptors, EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
    {
        foreach (ValidationDescriptor descriptor in validationDescriptors)
        {
            var argument = invocationContext.Arguments[descriptor.ArgumentIndex];
            if (argument is not null)
            {
                if (descriptor.Validate)
                {
                    var validatorType = validatorTypes.GetOrAdd(argument.GetType(), t => typeof(IValidator<>).MakeGenericType(t));
                    var validator = invocationContext.HttpContext.RequestServices.GetRequiredService(validatorType) as IValidator;
                    if (validator == null)
                    {
                        throw new InvalidOperationException($"Missing validator class {validatorType}.");
                    }

                    var validationResult = await validator.ValidateAsync(
                        new ValidationContext<object>(argument)
                    );

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary(), statusCode: (int)HttpStatusCode.UnprocessableEntity);
                    }
                }
            }
            else
            {
                if (descriptor.Required)
                {
                    return Results.ValidationProblem(
                        errors: new Dictionary<string, string[]>()
                        {
                            { descriptor.Name,  new [] { "is required." } }
                        }, 
                        statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }
            }
        }

        return await next.Invoke(invocationContext);
    }

    static IEnumerable<ValidationDescriptor> GetValidators(MethodInfo methodInfo)
    {
        var parameters = methodInfo.GetParameters();
        for (int i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];
            var required = parameter.GetCustomAttribute<RequiredAttribute>() != null;
            var validate = parameter.GetCustomAttribute<ValidateAttribute>() != null;
            if (required || validate)
            {
                yield return new ValidationDescriptor { Name = parameter.Name!, ArgumentIndex = i, Required = required, Validate = validate };
            }
        }
    }

    private class ValidationDescriptor
    {
        public required string Name { get; init; }
        public required int ArgumentIndex { get; init; }
        public required bool Required { get; init; }
        public required bool Validate { get; init; }
    }
}