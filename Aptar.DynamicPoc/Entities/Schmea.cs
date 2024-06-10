
namespace Aptar.DynamicPoc.Entities
{
    public class FormSchmea
    {
        public List<Field> Fields { get; set; } = new List<Field>();

        public Field AddField(string key, string type, Dictionary<string, object>? properties, List<Validator>? validators)
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
        public List<Validator> Validators { get; set; }
    }

    public class Option
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class Validator
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
