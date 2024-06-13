using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class DateRangeRule : ValidationRule
    {
        public DateTime? MinDate { get; private set; }
        public DateTime? MaxDate { get; private set; }

        public DateRangeRule(DateTime? minDate = default, DateTime? maxDate = default, string? message = default) : base("DateRange", message)
        {
            MinDate = minDate;
            MaxDate = maxDate;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, Field field, JObject model)
        {
            string key = field.Key;

            if (MinDate.HasValue || MaxDate.HasValue)
            {
                validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToObject<DateTime?>() : null)
                    .Must(date => !MinDate.HasValue || date >= MinDate.Value)
                    .WithMessage($"{key} must be on or after {MinDate?.ToShortDateString()}.")
                    .Must(date => !MaxDate.HasValue || date <= MaxDate.Value)
                    .WithMessage($"{key} must be on or before {MaxDate?.ToShortDateString()}.");
            }
        }
    }
}
