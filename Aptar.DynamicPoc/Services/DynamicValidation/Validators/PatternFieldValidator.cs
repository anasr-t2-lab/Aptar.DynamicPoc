using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class PatternFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions)
        {
            if (templateOptions["pattern"] is null)
                return;

            string pattern = templateOptions["pattern"]?.Value<string>();
            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                .Matches(pattern)
                .WithMessage($"{key} must match the pattern {pattern}.");
        }
    }
}
