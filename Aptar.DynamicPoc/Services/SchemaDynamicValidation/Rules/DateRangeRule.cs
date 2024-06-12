using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules
{
    public class DateRangeRule : ValidationRule
    {
        private readonly DateTime? _minDate;
        private readonly DateTime? _maxDate;

        public DateRangeRule(DateTime? minDate, DateTime? maxDate, string? message = default) : base(message)
        {
            _minDate = minDate;
            _maxDate = maxDate;
        }

        public override void ApplyRules(AbstractValidator<JObject> validator, string key, JObject model)
        {
            if (_minDate.HasValue || _maxDate.HasValue)
            {
                validator.RuleFor(x => x.ContainsKey(key) ? x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToObject<DateTime?>() : null)
                    .Must(date => !_minDate.HasValue || date >= _minDate.Value)
                    .WithMessage($"{key} must be on or after {_minDate?.ToShortDateString()}.")
                    .Must(date => !_maxDate.HasValue || date <= _maxDate.Value)
                    .WithMessage($"{key} must be on or before {_maxDate?.ToShortDateString()}.");
            }
        }
    }
}
