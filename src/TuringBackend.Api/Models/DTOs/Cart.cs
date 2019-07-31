using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class Cart
    {
        [JsonProperty("item_id")] 
        public int ItemId { get; set; }

        public string Name { get; set; }

        public string Attributes { get; set; }

        public string Price { get; set; }

        public int Quantity { get; set; }

        public string SubTotal { get; set; }
    }
}