using Aptar.DynamicPoc.Services.DynamicValidation.Interfaces;
using Aptar.DynamicPoc.Services.DynamicValidation.Validators;

namespace Aptar.DynamicPoc.Services.DynamicValidation
{
    public static class FieldValidatorFactory
    {
        private static readonly Dictionary<string, List<IFieldValidator>> ValidatorsMap = new Dictionary<string, List<IFieldValidator>>
    {
        { "input", new List<IFieldValidator>
        {
            new RequiredFieldValidator(),
            new MaxLengthFieldValidator(),
            new EmailFieldValidator(),
            new NumberFieldValidator(),
            new RangeFieldValidator(),
            new PatternFieldValidator()
        } },
        { "textarea", new List<IFieldValidator> { new RequiredFieldValidator(), new MaxLengthFieldValidator() } },
        { "select", new List<IFieldValidator> { new RequiredFieldValidator(), new SelectFieldValidator() } },
        { "date", new List<IFieldValidator> { new RequiredFieldValidator(), new DateFieldRangeValidator() } }
    };

        public static IEnumerable<IFieldValidator> GetValidators(string fieldType/*, JObject templateOptions*/)
        {
            if (ValidatorsMap.TryGetValue(fieldType, out var validators))
            {
                return validators;
            }

            return Enumerable.Empty<IFieldValidator>();
        }
    }
}
