using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public JObject Body { get; set; }
    }
}
