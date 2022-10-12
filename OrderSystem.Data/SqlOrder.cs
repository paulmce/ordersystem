using Microsoft.EntityFrameworkCore;
using OrderSystem.Contracts;
using OrderSystem.Models;
using System.Threading.Tasks;

namespace OrderSystem.Data
{
    public class SqlOrder : IOrderRepo
    {
        private readonly OrderDBContext _context;
        public SqlOrder(OrderDBContext context)
        {
            this._context = context;

        }

        public async Task<Order> AddNewOrderAsync(Order order)
        {
            var newOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return newOrder.Entity;
        }

        public async Task<int?> DeleteOrderAsync(int orderID)
        {
            var order = await _context.Orders.Where(t => t.Id == orderID).Include(order => order.Items).FirstOrDefaultAsync();
            if (order != null)
            {
                //cleanup order items 
                foreach (Item item in order.Items)
                {
                    _context.Items.Remove(item);
                }
                _context.Orders.Remove(order);
                
                await _context.SaveChangesAsync();
                return order.Id;
            }
            return null;
        }

        public async Task<Order> GetOrderByIDAsync(int orderID)
        {
            return await _context.Orders.Where(t => t.Id == orderID)
                .Include(order => order.Items)
                .FirstOrDefaultAsync();
            
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(order => order.Items)
                .ToListAsync();
        }

        public async Task<Order> UpdateOrderDetailsAsync(int orderID, Order order)
        {
            var orderToUpdate = _context.Orders.Where(t => t.Id == orderID).Include(order => order.Items).FirstOrDefault();
            if (orderToUpdate != null)
            {
                orderToUpdate.CustomerName = order.CustomerName;
                orderToUpdate.Address1 = order.Address1;
                orderToUpdate.Address2 = order.Address2;

                //remove deleted items
                foreach(Item item in orderToUpdate.Items)
                {
                    if (!order.Items.Any(t => t.ItemId == item.ItemId))
                        _context.Items.Remove(item);
                }

                foreach (var newItem in order.Items)
                {
                    var existingItem = orderToUpdate.Items
                        .Where(t => t.ItemId == newItem.ItemId && t.ItemId != default(int))
                        .SingleOrDefault();

                    if (existingItem != null)
                        //update items
                        _context.Entry(existingItem).CurrentValues.SetValues(newItem);
                    else
                    {
                        //insert new items
                        var insertitem = new Item
                        {
                            OrderId = newItem.OrderId, ProductId = newItem.ProductId, ProductQuantity = newItem.ProductQuantity
                            
                        };
                        orderToUpdate.Items.Add(insertitem);
                    }
                }

                await _context.SaveChangesAsync();
                return orderToUpdate;
            }
            return null;
        }

        public async Task<bool> CheckOrderExistsAsync(int orderID)
        {
            var orderCount = await _context.Orders.CountAsync(t => t.Id == orderID);
            if (orderCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Order>> GetPagedOrdersAsync(int pageNo, int noOrders)
        {
            return await _context.Orders
                .Include(order => order.Items)
                .Skip((pageNo - 1) * noOrders)
                .Take(noOrders)
                .ToListAsync();
        }
    }
}
