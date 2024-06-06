using Aptar.DynamicPoc.ViewModels;
using System.Text.Json;

namespace Aptar.DynamicPoc.Entities
{
    public class VendorValidation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public JsonDocument FormSchema { get; set; }

        public string GetFormSchemaAsString()
        {
            return JsonSerializer.Serialize(FormSchema.RootElement);
        }
    }


    //public class Pump
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //    public List<Material> AvailableMaterials { get; set; }
    //    public List<Color> AvailableColors { get; set; }
    //}
}
