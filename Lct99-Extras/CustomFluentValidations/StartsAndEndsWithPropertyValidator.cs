using FluentValidation;
using FluentValidation.Validators;

namespace CustomFluentValidations;

public class StartsAndEndsWithPropertyValidator<T> : PropertyValidator<T, string?>
{
    private readonly string _prefix;
    private readonly string _suffix;

    public StartsAndEndsWithPropertyValidator(string prefix, string suffix)
    {
        _prefix = prefix ?? string.Empty;
        _suffix = suffix ?? string.Empty;
    }

    public override string Name => "StartsAndEndsWithPropertyValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        context.MessageFormatter
            .AppendPropertyName(context.PropertyPath)
            .AppendArgument("Prefix", _prefix)
            .AppendArgument("Suffix", _suffix);

        return value.StartsWith(_prefix, StringComparison.Ordinal) && value.EndsWith(_suffix, StringComparison.Ordinal);
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} must start with '{Prefix}' and end with '{Suffix}'.";
}
