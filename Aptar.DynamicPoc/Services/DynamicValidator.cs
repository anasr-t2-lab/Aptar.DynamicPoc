using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Services
{
    public class DynamicValidator : AbstractValidator<JObject>
    {
        public DynamicValidator(JArray formlyTemplate)
        {
            foreach (var field in formlyTemplate)
            {
                string key = field["key"].ToString();
                string type = field["type"].ToString();
                var props = field["props"];

                if (props["required"] != null && props["required"].Value<bool>())
                {
                    RuleFor(x => x.GetValue(key,StringComparison.OrdinalIgnoreCase).ToString())
                        .NotEmpty()
                        .WithMessage($"{key} is required.");
                }

                if (props["maxLength"] != null)
                {
                    int maxLength = props["maxLength"].Value<int>();
                    RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                        .MaximumLength(maxLength)
                        .WithMessage($"{key} must not exceed {maxLength} characters.");
                }

                if (props["min"] != null || props["max"] != null)
                {
                    int min = props["min"] != null ? props["min"].Value<int>() : int.MinValue;
                    int max = props["max"] != null ? props["max"].Value<int>() : int.MaxValue;
                    RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).Value<int>())
                        .InclusiveBetween(min, max)
                        .WithMessage($"{key} must be between {min} and {max}.");
                }

                if (props["pattern"] != null)
                {
                    string pattern = props["pattern"].ToString();
                    RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                        .Matches(pattern)
                        .WithMessage($"{key} must match the pattern {pattern}.");
                }

                if (props["type"] != null && props["type"].Value<string>().ToLower() == "email")
                {
                    RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToString())
                        .EmailAddress()
                        .WithMessage($"{key} must be a valid email address.");
                }

                if (type == "select" && props["options"] != null)
                {

                    var options = props["options"]
                    .Select(o => o["value"].Value<int>())
                    .ToList();

                    RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase))
                        .Must(value => options.Contains(value.Value<int>()))
                        .WithMessage($"{key} must be one of the predefined options.");
                }

                if (type == "date")
                {
                    DateTime? minDate = props["minDate"]?.ToObject<DateTime>();
                    DateTime? maxDate = props["maxDate"]?.ToObject<DateTime>();

                    if (minDate.HasValue || maxDate.HasValue)
                    {
                        RuleFor(x => x.GetValue(key, StringComparison.OrdinalIgnoreCase).ToObject<DateTime>())
                            .Must(date => !minDate.HasValue || date >= minDate.Value)
                            .WithMessage($"{key} must be on or after {minDate?.ToShortDateString()}.")
                            .Must(date => !maxDate.HasValue || date <= maxDate.Value)
                            .WithMessage($"{key} must be on or before {maxDate?.ToShortDateString()}.");
                    }
                }
            }
        }
    }
}
