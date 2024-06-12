using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class MinRule : ValidationRule
    {
        private readonly double _min;

        public MinRule(double min, string? message = default) : base(message)
        {
            _min = min;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<double>() : default)
                .GreaterThanOrEqualTo(_min)
                .WithMessage($"{key} must be more than {_min}.");
        }
    }

}
