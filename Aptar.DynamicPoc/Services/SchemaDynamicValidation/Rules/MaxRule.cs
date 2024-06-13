using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class MaxRule : ValidationRule
    {
        public double Max { get; private set; }

        public MaxRule(double max, string? message = default) : base("Max", message)
        {
            Max = max;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<double>() : default)
                .LessThanOrEqualTo(Max)
                .WithMessage($"{key} must be less than {Max}.");
        }
    }

}
