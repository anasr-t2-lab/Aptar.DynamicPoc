using Newtonsoft.Json.Linq;

namespace Aptar.DynamicPoc.Entities
{
    public class AptarRequest
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public JObject Body { get; set; }
    }
}
