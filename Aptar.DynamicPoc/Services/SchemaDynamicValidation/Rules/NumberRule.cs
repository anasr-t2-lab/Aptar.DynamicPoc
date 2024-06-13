using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class NumberRule : ValidationRule
    {
        public NumberRule(string? message = default) : base("Number", message)
        {
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                .Must(value => value != null && double.TryParse(value.ToString(), out _))
                .WithMessage($"{key} must be a number.");
        }
    }
}
