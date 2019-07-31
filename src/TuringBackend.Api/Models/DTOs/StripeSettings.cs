namespace TuringBackend.Models
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
        public string WebhooksKey { get; set; }
    }
}