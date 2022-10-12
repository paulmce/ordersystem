using OrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderSystem.Data
{
    public class DataSeeder
    {
        private readonly OrderDBContext _orderDbContext;

        public DataSeeder(OrderDBContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public void Seed()
        {
            if (!_orderDbContext.Orders.Any())
            {
                var order1 = new Order()
                {
                    CreatedDate = DateTime.Now,
                    CustomerName = "Paul McElhinney",
                    Address1 = "1 my place",
                    Address2 = "Letterkenny",
                    Items = new Item[] { 
                           new Item { ProductId = 1, ProductQuantity=2 },
                           new Item { ProductId = 2, ProductQuantity=10 }
                            }
                };

                _orderDbContext.Orders.Add(order1);

                var order2 = new Order()
                {
                    CreatedDate = DateTime.Now,
                    CustomerName = "Joe Bloggs",
                    Address1 = "1 Joes Place",
                    Address2 = "Letterkenny",
                    Items = new Item[] {
                           new Item { ProductId = 1, ProductQuantity=13 },
                           new Item { ProductId = 2, ProductQuantity=11 }
                            }
                };

                _orderDbContext.Orders.Add(order2);

                var order3 = new Order()
                {
                    CreatedDate = DateTime.Now,
                    CustomerName = "Patrick Ryan",
                    Address1 = "1 Pats Place",
                    Address2 = "Letterkenny",
                    Items = new Item[] {
                           new Item { ProductId = 1, ProductQuantity=5 },
                           new Item { ProductId = 2, ProductQuantity=9 },
                           new Item { ProductId = 3, ProductQuantity=9 }
                            }
                };

                _orderDbContext.Orders.Add(order3);
                
                var order4 = new Order()
                {
                    CreatedDate = DateTime.Now,
                    CustomerName = "Lisa Lyons",
                    Address1 = "1 Lisas Place",
                    Address2 = "Letterkenny",
                    Items = new Item[] {
                           new Item { ProductId = 1, ProductQuantity=5 },
                           new Item { ProductId = 2, ProductQuantity=9 },
                           new Item { ProductId = 3, ProductQuantity=9 },
                           new Item { ProductId = 4, ProductQuantity=10 }
                            }
                };

                _orderDbContext.Orders.Add(order4);

                _orderDbContext.SaveChanges();
            }
        }
    }
}
