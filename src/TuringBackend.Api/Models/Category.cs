using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class Category
    {
        /// <summary>
        /// </summary>
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("department_id")] public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}