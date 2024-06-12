using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation
{
    public class FormSchmea
    {
        public List<Field> Fields { get; set; } = new List<Field>();

        public Field AddField(string key, string type, Dictionary<string, object>? properties, List<ValidationRule>? validators)
        {
            var field = new Field { Key = key, Type = type, Properties = properties, Validators = validators };
            Fields.Add(field);
            return field;
        }
    }

    public class Field
    {
        public string Key { get; set; }
        public string Type { get; set; } // todo add detailed type
        public Dictionary<string, object> Properties { get; set; }
        public List<Option> Options { get; set; } // select options
        public List<ValidationRule> Validators { get; set; }
    }

    public class Option
    {
        public string Label { get; set; }
        public object Value { get; set; }
    }
}
