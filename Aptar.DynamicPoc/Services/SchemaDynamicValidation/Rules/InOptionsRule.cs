using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class InOptionsRule : ValidationRule
    {
        public InOptionsRule(string? message = default) : base("InOptions", message)
        {
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;
            
            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase))
                .Must(value => field.Options.Select(o => o.Value.ToString().ToLower()).Contains(value.Value<string>().ToLower()))
                .WithMessage($"{key} must be one of the predefined options.");
        }
    }
}
