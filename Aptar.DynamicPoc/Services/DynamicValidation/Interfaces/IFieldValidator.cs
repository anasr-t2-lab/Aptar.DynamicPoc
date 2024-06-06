using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Interfaces
{
    public interface IFieldValidator
    {
        void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions);
    }
}
