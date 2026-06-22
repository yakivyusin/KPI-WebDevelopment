using FluentValidation;
using Microsoft.Extensions.Options;

namespace Options_Extra.Options;

public class MySectionValidator : AbstractValidator<MySection>, IValidateOptions<MySection>
{
    public MySectionValidator()
    {
        RuleFor(x => x.MySubsection).NotEmpty();
    }

    public ValidateOptionsResult Validate(string? name, MySection options)
    {
        var result = Validate(options);

        return result.IsValid ?
            ValidateOptionsResult.Success :
            ValidateOptionsResult.Fail(result.ToString());
    }
}
