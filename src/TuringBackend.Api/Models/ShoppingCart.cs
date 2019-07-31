using System;
using System.Threading.Tasks;

namespace TuringBackend.Models
{
    public class ShoppingCart
    {
        public int ItemId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public string Attributes { get; set; }
        public int Quantity { get; set; }
        public sbyte BuyNow { get; set; }
        public DateTime AddedOn { get; set; }

        
    }
}