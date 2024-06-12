using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class RquiredRule : ValidationRule
    {
        private readonly string _expression;

        public RquiredRule(string? expression = default, string? message = default) : base(message)
        {
            _expression = expression;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {
            if (!string.IsNullOrWhiteSpace(_expression) && model is not null)
            {
                validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                   .NotEmpty()
                   .When(x => ExpressionLogicalEvaluator.Evaluate(model, _expression))
                   .WithMessage(Message ?? $"{key} is required.");
            }
            else
            {
                validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                   .NotEmpty()
                   .WithMessage(Message ?? $"{key} is required.");
            }
        }
    }
}
