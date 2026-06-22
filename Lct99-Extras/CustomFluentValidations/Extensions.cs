using FluentValidation;

namespace CustomFluentValidations;

public static class Extensions
{
    public static IRuleBuilderOptionsConditions<T, string?> StartsEndsWith<T>(this IRuleBuilder<T, string?> ruleBuilder, string prefix, string suffix)
    {
        return ruleBuilder.Custom((property, context) =>
        {
            context.MessageFormatter.AppendPropertyName(context.PropertyPath);
            context.MessageFormatter.AppendArgument("Prefix", prefix);
            context.MessageFormatter.AppendArgument("Suffix", suffix);

            if (!(property?.StartsWith(prefix) ?? false))
            {
                context.AddFailure("{PropertyName} must start with '{Prefix}'");
            }

            if (!(property?.EndsWith(suffix) ?? false))
            {
                context.AddFailure("{PropertyName} must end with '{Suffix}'");
            }
        });
    }
}
