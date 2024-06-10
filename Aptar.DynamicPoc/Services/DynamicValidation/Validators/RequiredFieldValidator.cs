using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class RequiredFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject templateOptions)
        {
            //if (templateOptions["required"]?.Value<bool>() == true)
            //{
                validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                        .NotEmpty()
                        .WithMessage($"{key} is required.");
                //validator.RuleFor(x => ((JObject)(object)x)[key])
                //    .NotEmpty()
                //    .WithMessage($"{key} is required.");
            //}
        }
    }
}
