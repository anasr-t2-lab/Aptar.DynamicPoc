using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;

namespace Aptar.DynamicPoc.Data.Extensions
{
    public static class ValidationRuleTypeRegistry
    {
        private static readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>();

        static ValidationRuleTypeRegistry()
        {
            RegisterType<RequiredRule>();
            RegisterType<DateRangeRule>();
            RegisterType<EmailRule>();
            RegisterType<InOptionsRule>();
            RegisterType<MaxLengthRule>();
            RegisterType<MaxRule>();
            RegisterType<MinRule>();
            RegisterType<NumberRule>();
            RegisterType<PatternRule>();
            RegisterType<RangeRule>();
        }

        public static void RegisterType<T>() where T : ValidationRule
        {
            TypeMap[typeof(T).Name] = typeof(T);
        }

        public static Type GetType(string typeName)
        {
            return TypeMap.TryGetValue(typeName, out var type) ? type : null;
        }
    }
}