using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class AttributeValueByAttribute
    {
        [JsonProperty("attribute_value_id")] 
        public int AttributeValueId { get; set; }

        public string Value { get; set; }
    }
}