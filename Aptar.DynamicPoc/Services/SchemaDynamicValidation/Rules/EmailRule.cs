using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class EmailRule : ValidationRule
    {
        public EmailRule(string? message = default) : base("Email", message)
        {
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                   .EmailAddress()
                   .WithMessage($"{key} must be a valid email address.");
        }
    }
}
