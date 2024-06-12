using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class MaxRule : ValidationRule
    {
        private readonly double _max;

        public MaxRule(double max, string? message = default) : base(message)
        {
            _max = max;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<double>() : default)
                .LessThanOrEqualTo(_max)
                .WithMessage($"{key} must be less than {_max}.");
        }
    }

}
