using CodingSeb.ExpressionEvaluator;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services
{
    public static class ExpressionLogicalEvaluator
    {
        public static bool Evaluate(JObject jObject, string expression)
        {
            ExpressionEvaluator evaluator = new ExpressionEvaluator();

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
                evaluator.Variables[propertyName] = propertyValue;
            }

            return (bool)evaluator.Evaluate(expression);
        }
    }
}