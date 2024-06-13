using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class RangeRule : ValidationRule
    {
        public double Min { get; private set; }
        public double Max { get; private set; }

        public RangeRule(double min, double max, string? message = default) : base("Range", message)
        {
            Min = min;
            Max = max;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<double>() : default)
            .InclusiveBetween(Min, Max)
            .WithMessage($"{key} must be between {Min} and {Max}.");
        }
    }

}
