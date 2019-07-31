using System;
using Newtonsoft.Json;

namespace TuringBackend.Models
{
    public class Review
    {
        [JsonProperty("review_id")] public int ReviewId { get; set; }

        [JsonProperty("customer_id")] public int CustomerId { get; set; }

        [JsonProperty("product_id")] public int ProductId { get; set; }

        [JsonProperty("review")] public string Review1 { get; set; }

        public short Rating { get; set; }

        [JsonProperty("review_id")] public DateTime CreatedOn { get; set; }
    }
}