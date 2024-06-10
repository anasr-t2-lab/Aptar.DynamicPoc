using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation
{
    public class DynamicRequestValidator : AbstractValidator<JObject>
    {
        public DynamicRequestValidator(JArray formlyTemplate, JObject model)
        {
            foreach (var field in formlyTemplate)
            {
                string key = field["key"].ToString();
                string type = field["type"].ToString();
                var props = (JObject)field["props"];
                var validators = (JObject)field["validators"];
                var expressions = (JObject)field["expressions"];


                // appraoch 1
                //var validators = FieldValidatorFactory.GetValidators(type);

                //foreach (var validator in validators)
                //{
                //    validator.ApplyRules<JObject>(this, key, props);
                //}

                // appraoch 2

                var strategies = ValidationStrategyFactory.GetStrategies(model, props, validators, expressions).ToList();

                foreach (var strategy in strategies)
                {
                    strategy.ApplyRules<JObject>(this, key, props);
                }
            }
        }
    }
}
