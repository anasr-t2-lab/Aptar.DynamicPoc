using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using Aptar.DynamicPoc.Services.DynamicValidation.Validators;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Aptar.DynamicPoc.Services.DynamicValidation
{
    public static class ValidationStrategyFactory
    {
        private static readonly Dictionary<string, IFieldValidator> StrategyMap = new()
        {
            { "required", new RequiredFieldValidator() },
            { "props.required", new RequiredFieldValidator() },
            { "maxLength", new MaxLengthFieldValidator() },
            { "pattern", new PatternFieldValidator() },
            { "email",new EmailFieldValidator() },
            { "options", new SelectFieldValidator() },
            { "minDate", new DateFieldRangeValidator() },
            { "maxDate", new DateFieldRangeValidator() },
            { "min", new RangeFieldValidator() },
            { "max", new RangeFieldValidator() },
            { "ipValidator", new IpFieldValidator() }
        };

        public static List<IFieldValidator> GetStrategies(JObject model, JObject fieldProps, JObject fieldValidators, JObject fieldExpressions)
        {
            List<string> validatorsKeys = new();
            foreach (var property in fieldProps)
            {
                validatorsKeys.Add(property.Key == "type" ? property.Value.Value<string>() : property.Key);
            }

            if (fieldValidators?["validation"]!=null)
                validatorsKeys.AddRange(fieldValidators["validation"].Values<string>());

            // Expressions
            if (fieldExpressions != null)
            {
                foreach (var expression in fieldExpressions)
                {
                    //string expressionValue = FormatExpression(expression.Value.ToString());
                    string expressionValue = Replace(expression.Value.ToString(), @"\bmodel\.");
                    if (ExpressionLogicalEvaluator.Evaluate(model, expressionValue))
                    {
                        validatorsKeys.Add(expression.Key);
                    }
                }
            }

            return GetValidators(validatorsKeys.Distinct());
        }

        public static List<IFieldValidator> GetValidators(IEnumerable<string> validatorsKeys)
        {
            Dictionary<Type,IFieldValidator> validators = new();
            foreach (var key in validatorsKeys)
            {
                if (StrategyMap.TryGetValue(key, out var strategy) && !validators.ContainsKey(strategy.GetType()))
                    validators.Add(strategy.GetType(), strategy);
            }
            return validators.Values.ToList();
        }

        private static string FormatExpression(string input)
        {
            // Step 1: Remove "model."
            string withoutModel = Replace(input, @"\bmodel\.");

            // Step 2: Convert to Pascal case
            // Split the input into words and operators
            var matches = Regex.Matches(withoutModel, @"(!?[a-zA-Z_]\w*|&&|\|\||[!><=]+|\s+|\d+)");

            StringBuilder result = new StringBuilder();
            foreach (Match match in matches)
            {
                string value = match.Value;

                // Check if it's a property name (word) and not an operator or number
                if (Regex.IsMatch(value, @"^[a-zA-Z_]\w*$") || Regex.IsMatch(value, @"^![a-zA-Z_]\w*$"))
                {
                    if (value.StartsWith("!"))
                    {
                        value = "!" + char.ToUpperInvariant(value[1]) + value.Substring(2);
                    }
                    else
                    {
                        value = char.ToUpperInvariant(value[0]) + value.Substring(1);
                    }
                }

                result.Append(value);
            }

            return result.ToString();
        }

        private static string Replace(string input, string pattern)
        {
            string withoutModel = Regex.Replace(input, pattern, string.Empty);
            return withoutModel;
        }
    }

}
