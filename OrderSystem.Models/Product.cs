using System.ComponentModel.DataAnnotations.Schema;

namespace OrderSystem.Models
{
    public class Product
    {
        //NOT USING PRODUCT YET
        public int Id { get; set; }
        public string? Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int StockCount { get; set; }
    }
}
