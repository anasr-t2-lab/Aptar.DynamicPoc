using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation
{
    public class Field
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string UiType { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public List<Option> Options { get; set; } // select options
        public List<ValidationRule> ValidationRules { get; set; }
    }

    public class Option
    {
        public string Label { get; set; }
        public object Value { get; set; }
    }
}
