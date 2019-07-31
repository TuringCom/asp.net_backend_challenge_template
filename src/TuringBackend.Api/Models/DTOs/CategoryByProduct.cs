using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class CategoryBasic
    {
        /// <summary>
        ///     example: 1
        /// </summary>
        [JsonProperty("department_id")]
        public int DepartmentId { get; set; }

        /// <summary>
        ///     example: 1
        /// </summary>
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        /// <summary>
        ///     example: Regional
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}