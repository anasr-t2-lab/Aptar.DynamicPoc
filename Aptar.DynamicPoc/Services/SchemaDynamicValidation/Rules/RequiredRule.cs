using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class RequiredRule : ValidationRule
    {
        public string Expression { get; private set; }

        public RequiredRule(string? expression = default, string? message = default) : base("Required", message)
        {
            Expression = expression;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;
            if (!string.IsNullOrWhiteSpace(Expression) && model is not null)
            {
                validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString() : default)
                   .NotEmpty()
                   .When(x => ExpressionLogicalEvaluator.Evaluate(model, Expression))
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
