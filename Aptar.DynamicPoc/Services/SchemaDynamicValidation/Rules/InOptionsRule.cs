using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class InOptionsRule : ValidationRule
    {
        private readonly List<Option> _options;

        public InOptionsRule(List<Option> options, string? message = default) : base(message)
        {
            _options = options;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {
            validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase))
                .Must(value => _options.Select(o => o.Value.ToString().ToLower()).Contains(value.Value<string>().ToLower()))
                .WithMessage($"{key} must be one of the predefined options.");
        }
    }
}
