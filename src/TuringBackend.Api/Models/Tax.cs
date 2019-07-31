namespace TuringBackend.Models
{
    public class Tax
    {
        public int TaxId { get; set; }
        public string TaxType { get; set; }
        public decimal TaxPercentage { get; set; }
    }
}