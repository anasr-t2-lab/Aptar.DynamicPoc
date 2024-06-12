using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class PatternRule : ValidationRule
    {
        private readonly string _pattern;

        public PatternRule(string pattern, string? message = default) : base(message)
        {
            _pattern = pattern;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {
            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
            .Matches(_pattern)
            .WithMessage($"{key} must match the pattern {_pattern}.");
        }
    }
}
