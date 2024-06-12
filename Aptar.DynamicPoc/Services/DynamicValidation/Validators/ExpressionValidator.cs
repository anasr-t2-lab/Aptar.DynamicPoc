using CodingSeb.ExpressionEvaluator;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class ExpressionValidator : AbstractValidator<JObject>
    {
        private readonly ExpressionEvaluator _evaluator;
        public List<string> Rules { get; set; } = new();
        public ExpressionValidator()
        {
            _evaluator = new ExpressionEvaluator();
        }

        public void AddRule(string expression, string errorMessage)
        {
            Rules.Add(expression);
            RuleFor(jObject => jObject)
                .Must(jObject => EvaluateLogicalExpression(jObject, expression))
                .WithMessage(errorMessage);
        }

        private bool EvaluateLogicalExpression(JObject jObject, string expression)
        {
            foreach (var property in jObject.Properties())
            {
                string propertyName = property.Name;
                object propertyValue = property.Value.Type switch
                {
                    JTokenType.String => property.Value.ToString(),
                    JTokenType.Boolean => (bool)property.Value,
                    JTokenType.Integer => (int)property.Value,
                    JTokenType.Float => (double)property.Value,
                    _ => property.Value.ToString()
                };
                _evaluator.Variables[propertyName] = propertyValue;
            }

            return (bool)_evaluator.Evaluate(expression);
        }
    }
}