namespace Aptar.DynamicPoc.ViewModels
{
    public class ColorRequest
    {
        public ColorType ColorType { get; set; }
        public string Color { get; set; }
        public string PartNumber { get; set; }
        public int TranslucencePercentage { get; set; }
        public bool SampleSubmission { get; set; }
        public string ShippingAddress { get; set; }
        public string Ip { get; set; }

        public bool IsShippingAddressRequired() => SampleSubmission;
    }

    public enum ColorType
    {
        Pantone = 1,
        Hex,
        RGB,
        CMYK
    }
}
