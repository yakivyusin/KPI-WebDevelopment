using System.ComponentModel.DataAnnotations;

namespace DataAnnotations_Minimal;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder)
    {
        builder.AddEndpointFilter(async (invocationContext, next) =>
        {
            var argument = invocationContext.Arguments.OfType<T>().FirstOrDefault();

            if (argument == null)
            {
                return await next(invocationContext);
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(argument);
            var isValid = Validator.TryValidateObject(argument, context, results, true);

            if (!isValid)
            {
                var errors = results.GroupBy(x => string.Join(", ", x.MemberNames))
                    .ToDictionary(
                        x => x.Key,
                        x => x.Select(g => g.ErrorMessage ?? string.Empty).ToArray());

                return Results.ValidationProblem(errors);
            }

            return await next(invocationContext);
        });

        return builder;
    }
}
