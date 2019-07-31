using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class ProductDetail
    {
        [JsonProperty("product_id")] 
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        [JsonProperty("discounted_price")] 
        public decimal DiscountedPrice { get; set; }

        public string Image { get; set; }

        public string Image2 { get; set; }
    }
}