using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using Aptar.DynamicPoc.Services.DynamicValidation.Validators;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation
{
    public static class ValidationStrategyFactory
    {
        private static readonly Dictionary<string, IFieldValidator> StrategyMap = new()
        {
            { "required", new RequiredFieldValidator() },
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

        public static IEnumerable<IFieldValidator> GetStrategies(JObject props, JObject validators)
        {

            List<string> validatorsKeys = new();
            foreach (var property in props)
            {
                validatorsKeys.Add(property.Key == "type" ? property.Value.Value<string>() : property.Key);
            }

            if (validators?["validation"]!=null)
                validatorsKeys.AddRange(validators["validation"].Values<string>());

            return GetValidators(validatorsKeys.Distinct());
        }

        private static List<IFieldValidator> GetValidators(IEnumerable<string> validatorsKeys)
        {
            Dictionary<Type,IFieldValidator> validators = new();
            foreach (var key in validatorsKeys)
            {
                if (StrategyMap.TryGetValue(key, out var strategy) && !validators.ContainsKey(strategy.GetType()))
                    validators.Add(strategy.GetType(), strategy);
            }
            return validators.Values.ToList();
        }
    }

}
