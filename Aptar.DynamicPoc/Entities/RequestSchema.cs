using Aptar.DynamicPoc.Services.SchemaDynamicValidation;

namespace Aptar.DynamicPoc.Entities
{
    public class RequestSchema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }
}
