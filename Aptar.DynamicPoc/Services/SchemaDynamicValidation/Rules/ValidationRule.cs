using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class ValidationRule
    {
        protected string Message { get; set; }

        protected ValidationRule(string? message = default)
        {
            Message = message;
        }

        protected ValidationRule() { }

        public virtual void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model) { }
    }
}
