using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Aptar.DynamicPoc.Data.Extensions
{
    //public class ValidationRuleConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(ValidationRule).IsAssignableFrom(objectType);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        //JObject jsonObject = JObject.Load(reader);
    //        //var type = (string)jsonObject["Type"];
    //        //var payloadType = (string)jsonObject["PayloadType"];
    //        //ValidationRule rule;

    //        //if (type == null || payloadType == null)
    //        //{
    //        //    return null;
    //        //}

    //        //switch (type)
    //        //{
    //        //    case nameof(RequiredRule):
    //        //        rule = new RequiredRule((string)jsonObject["Expression"]);
    //        //        break;
    //        //    case nameof(RangeRule):
    //        //        rule = new RangeRule((double)jsonObject["Min"], (double)jsonObject["Max"]);
    //        //        break;
    //        //    default:
    //        //        throw new Exception($"Unknown rule type: {type}");
    //        //        break;
    //        //}

    //        //serializer.Populate(jsonObject.CreateReader(), rule);
    //        //return rule;

    //        return null;
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var rule = value as ValidationRule;
    //        var jsonObject = new JObject
    //    {
    //        { "Type", rule.GetType().Name }, // todo change to name
    //            { "PayloadType", rule.GetType().FullName}
    //    };

    //        if (rule is RequiredRule requiredRule)
    //        {
    //            jsonObject.Add("Expression", requiredRule.Expression);
    //        }
    //        else if (rule is MinRule minRule)
    //        {
    //            jsonObject.Add("Min", minRule.Min);
    //        }
    //        else if (rule is MaxRule maxRule)
    //        {
    //            jsonObject.Add("Max", maxRule.Max);
    //        }

    //        serializer.Serialize(writer, jsonObject);
    //    }
    //}

    public class ValidationRuleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ValidationRule).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string typeName = (string)jsonObject["PayloadType"];
            Type type = Type.GetType(typeName) ?? throw new Exception($"Unknown rule type: {typeName}");
            var rule = (ValidationRule)JsonConvert.DeserializeObject(jsonObject.ToString(), type);
            serializer.Populate(jsonObject.CreateReader(), rule);
            return rule;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rule = value as ValidationRule;
            JObject jsonObject = ConvertToJObject(rule);
            jsonObject.AddFirst(new JProperty("PayloadType", value.GetType().AssemblyQualifiedName));

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
