using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class IpFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions)
        {
            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
            .Matches("(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?(\\.|$)){4}")
                .WithMessage($"{key} must be a valid ip.");
        }
    }
}
