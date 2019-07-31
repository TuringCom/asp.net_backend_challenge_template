using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class CustomerReview
    {
        public string Name { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }

        [JsonProperty("created_on")] 
        public string CreatedOn { get; set; }
    }
}