using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class RangeRule : ValidationRule
    {
        private readonly double _min;
        private readonly double _max;

        public RangeRule(double min, double max, string? message = default) : base(message)
        {
            _min = min;
            _max = max;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<double>() : default)
            .InclusiveBetween(_min, _max)
            .WithMessage($"{key} must be between {_min} and {_max}.");
        }
    }

}
