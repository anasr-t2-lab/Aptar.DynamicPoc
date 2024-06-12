using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class MaxLengthRule : ValidationRule
    {
        private readonly int _maxLength;

        public MaxLengthRule(int maxLength, string? message = default) : base(message)
        {
            _maxLength = maxLength;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {
            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                .MaximumLength(_maxLength)
                .WithMessage($"{key} must not exceed {_maxLength} characters.");
        }
    }
}
