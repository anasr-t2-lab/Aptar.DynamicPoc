using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    public class MaxLengthFieldValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject props)
        {
            if (props["maxLength"] != null)
            {
                //int maxLength = templateOptions["maxLength"].Value<int>();
                //validator.RuleFor(x => ((JObject)(object)x)[key].ToString())
                //    .MaximumLength(maxLength)
                //    .WithMessage($"{key} must not exceed {maxLength} characters.");

                int maxLength = props["maxLength"].Value<int>();
                validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                    .MaximumLength(maxLength)
                    .WithMessage($"{key} must not exceed {maxLength} characters.");
            }
        }
    }
}
