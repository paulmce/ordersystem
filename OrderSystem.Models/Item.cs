using System.Text.Json.Serialization;

namespace OrderSystem.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        

    }
}
