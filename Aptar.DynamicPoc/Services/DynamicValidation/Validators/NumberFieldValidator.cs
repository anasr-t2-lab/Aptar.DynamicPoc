using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class NumberFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions)
        {
            if (templateOptions["type"]?.Value<string>() == "number")
            {
                validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                .Must(value => value != null && double.TryParse(value.ToString(), out _))
                .WithMessage($"{key} must be a number.");
            }
        }
    }
}
