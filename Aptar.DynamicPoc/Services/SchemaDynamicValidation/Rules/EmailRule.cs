using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class EmailRule : ValidationRule
    {
        public EmailRule(string? message = default) : base(message)
        {
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {
            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                   .EmailAddress()
                   .WithMessage($"{key} must be a valid email address.");
        }
    }
}
