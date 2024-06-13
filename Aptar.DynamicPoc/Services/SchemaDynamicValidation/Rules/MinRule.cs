using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class MinRule : ValidationRule
    {
        public double Min { get; private set; }

        public MinRule(double min, string? message = default) : base("Min", message)
        {
            Min = min;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<double>() : default)
                .GreaterThanOrEqualTo(Min)
                .WithMessage($"{key} must be more than {Min}.");
        }
    }

}
