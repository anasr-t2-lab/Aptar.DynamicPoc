using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class NumberRule : ValidationRule
    {
        public NumberRule(string? message = default) : base(message)
        {
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {
            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                .Must(value => value != null && double.TryParse(value.ToString(), out _))
                .WithMessage($"{key} must be a number.");
        }
    }
}
