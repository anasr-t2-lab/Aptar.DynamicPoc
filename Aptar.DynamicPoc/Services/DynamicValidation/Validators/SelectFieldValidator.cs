using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class SelectFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions)
        {
            if (templateOptions["type"]?.Value<string>() == "select")
                return;

            var options = new HashSet<int>(templateOptions["options"]
                .Select(o => o["value"].Value<int>()));

            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase))
                .Must(value => options.Contains(value.Value<int>()))
                .WithMessage($"{key} must be one of the predefined options.");
        }
    }
}
