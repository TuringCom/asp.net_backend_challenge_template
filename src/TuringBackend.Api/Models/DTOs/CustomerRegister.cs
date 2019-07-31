using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class CustomerRegister
    {
        public Customer Customer { get; set; }

        public string AccessToken { get; set; }

        [JsonProperty("expires_in")] 
        public string ExpiresIn { get; set; }
    }
}