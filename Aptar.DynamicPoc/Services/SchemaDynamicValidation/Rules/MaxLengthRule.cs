using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class MaxLengthRule : ValidationRule
    {
        public int MaxLength { get; private set; }

        public MaxLengthRule(int maxLength, string? message = default) : base("MaxLength", message)
        {
            MaxLength = maxLength;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                .MaximumLength(MaxLength)
                .WithMessage($"{key} must not exceed {MaxLength} characters.");
        }
    }
}
