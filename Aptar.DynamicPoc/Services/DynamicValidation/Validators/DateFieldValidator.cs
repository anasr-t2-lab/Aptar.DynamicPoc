using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.DynamicValidation.Validators
{
    //public class DateFieldValidator : IFieldValidator
    //{
    //    //public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject props)
    //    //{
    //    //    if (props["type"]?.Value<string>() != "date")
    //    //        return;


    //    //    validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToObject<DateTime>())
    //    //        .Must(date => 
    //    //}


    //}


    public class DateFieldRangeValidator : IFieldValidator
    {
        public void ApplyRules<T>(AbstractValidator<JObject> validator, string key, JObject props)
        {
            DateTime? minDate = props["minDate"]?.ToObject<DateTime>();
            DateTime? maxDate = props["maxDate"]?.ToObject<DateTime>();

            if (minDate.HasValue || maxDate.HasValue)
            {
                validator.RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToObject<DateTime>())
                    .Must(date => !minDate.HasValue || date >= minDate.Value)
                    .WithMessage($"{key} must be on or after {minDate?.ToShortDateString()}.")
                    .Must(date => !maxDate.HasValue || date <= maxDate.Value)
                    .WithMessage($"{key} must be on or before {maxDate?.ToShortDateString()}.");
            }
        }
    }
}
