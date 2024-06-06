using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation
{
    public class DynamicRequestValidator : AbstractValidator<JObject>
    {
        public DynamicRequestValidator(JArray formlyTemplate)
        {
            foreach (var field in formlyTemplate)
            {
                string key = field["key"].ToString();
                string type = field["type"].ToString();
                var props = (JObject)field["props"];
                var validators = (JObject)field["validators"];


                // appraoch 1
                //var validators = FieldValidatorFactory.GetValidators(type);

                //foreach (var validator in validators)
                //{
                //    validator.ApplyRules<JObject>(this, key, props);
                //}

                // appraoch 2
                var strategies = ValidationStrategyFactory.GetStrategies(props, validators);

                foreach (var strategy in strategies)
                {
                    strategy.ApplyRules<JObject>(this, key, props);
                }
            }
        }
    }
}
