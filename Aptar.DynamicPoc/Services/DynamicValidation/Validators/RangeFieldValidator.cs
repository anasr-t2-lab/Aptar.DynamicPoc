using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class RangeFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions)
        {
            if (templateOptions["min"] != null || templateOptions["max"] != null)
            {
                double min = templateOptions["min"]?.Value<double>() ?? double.MinValue;
                double max = templateOptions["max"]?.Value<double>() ?? double.MaxValue;

                validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<double>())
                    .InclusiveBetween(min, max)
                    .WithMessage($"{key} must be between {min} and {max}.");
            }
        }
    }
}
