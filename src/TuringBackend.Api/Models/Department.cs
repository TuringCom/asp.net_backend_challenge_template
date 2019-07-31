using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class Department
    {
        /// <summary>
        ///     example: 1
        /// </summary>
        [JsonProperty("department_id")]
        public int DepartmentId { get; set; }

        /// <summary>
        ///     example: Regional
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     example: Proud of your country? Wear a T-shirt with a national symbol stamp!
        /// </summary>
        public string Description { get; set; }
    }
}