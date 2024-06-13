using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation
{
    public class DynamicValidator : AbstractValidator<JObject>
    {
        public DynamicValidator(JObject model, List<Field> fields)
        {
            foreach (var field in fields)
            {
                field.ValidationRules?.ForEach(f => f.ApplyRules(this, field, model));
            }
        }
    }
}
