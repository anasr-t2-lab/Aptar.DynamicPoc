using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Entities
{
    public class RequestType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public JArray FormSchema { get; set; }

        //public string GetFormSchemaAsString()
        //{
        //    return JsonSerializer.Serialize(FormSchema.RootElement);
        //}
    }
}
