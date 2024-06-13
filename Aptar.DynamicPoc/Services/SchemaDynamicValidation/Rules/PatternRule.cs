using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class PatternRule : ValidationRule
    {
        public string Pattern { get; private set; }

        public PatternRule(string pattern, string? message = default) : base("Pattern", message)
        {
            Pattern = pattern;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
            .Matches(Pattern)
            .WithMessage($"{key} must match the pattern {Pattern}.");
        }
    }
}
