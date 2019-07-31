using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class ProductLocations
    {
        [JsonProperty("category_id")] 
        public int CategoryId { get; set; }


        [JsonProperty("category_name")] 
        public string CategoryName { get; set; }


        [JsonProperty("department_id")] 
        public int DepartmentId { get; set; }


        [JsonProperty("department_name")] 
        public string DepartmentName { get; set; }
    }
}