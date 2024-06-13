using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public abstract class ValidationRule
    {
        public string Type { get; private set; }
        public string Message { get; private set; }

        protected ValidationRule(string type, string? message = default)
        {
            Type = type;
            Message = message;
        }

        protected ValidationRule()
        {
        }

        public abstract void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model);
    }
}
