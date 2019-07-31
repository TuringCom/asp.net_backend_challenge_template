using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class ProductAttributeValue
    {
        [JsonProperty("attribute_value_id")] 
        public int AttributeValueId { get; set; }

        [JsonProperty("attribute_name")] 
        public string AttributeName { get; set; }

        [JsonProperty("attribute_value")] 
        public string AttributeValue { get; set; }
    }
}