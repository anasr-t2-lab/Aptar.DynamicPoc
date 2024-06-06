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
            .Matches("/(\\d{1,3}\\.){3}\\d{1,3}/")
                .WithMessage($"{key} must be a valid ip.");
        }
    }
}
