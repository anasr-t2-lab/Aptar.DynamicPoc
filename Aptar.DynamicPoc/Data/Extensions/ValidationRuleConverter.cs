using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Aptar.DynamicPoc.Data.Extensions
{


    public class ValidationRuleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ValidationRule).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string ruleTtpe = (string)jsonObject["RuleType"];
            Type type = ValidationRuleTypeRegistry.GetType(ruleTtpe) ?? throw new Exception($"Unknown rule type: {ruleTtpe}");
            var rule = (ValidationRule)JsonConvert.DeserializeObject(jsonObject.ToString(), type);
            serializer.Populate(jsonObject.CreateReader(), rule);
            return rule;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rule = value as ValidationRule;
            JObject jsonObject = ConvertToJObject(rule);
            jsonObject.AddFirst(new JProperty("RuleType", value.GetType().Name));

            jsonObject.WriteTo(writer);
        }

        public static JObject ConvertToJObject(object obj)
        {
            if (obj == null)
                return null;

            JObject jObject = new JObject();
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    var value = property.GetValue(obj);
                    jObject.Add(property.Name, value != null ? JToken.FromObject(value) : JValue.CreateNull());
                }
            }

            return jObject;
        }
    }

}
