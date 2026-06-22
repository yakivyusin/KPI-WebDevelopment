using FluentValidation;

namespace Common_FluentValidation;

public static class Extensions
{
    public static IRuleBuilderOptions<T, string?> StartsWith<T>(this IRuleBuilder<T, string?> ruleBuilder, string prefix)
    {
        return ruleBuilder.Must((@object, property, context) =>
        {
            context.MessageFormatter.AppendArgument("Prefix", prefix);
            return property?.StartsWith(prefix) ?? false;
        })
        .WithMessage("{PropertyName} must start with '{Prefix}'.");
    }
}
