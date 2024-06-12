using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class EmailFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions)
        {
            if (templateOptions["type"]?.Value<string>() != "email")
                return;

            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                    .EmailAddress()
                    .WithMessage($"{key} must be a valid email address.");

        }
    }
}
