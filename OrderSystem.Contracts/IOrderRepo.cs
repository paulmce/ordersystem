using OrderSystem.Models;

namespace OrderSystem.Contracts
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetOrdersAsync();
        Task<List<Order>> GetPagedOrdersAsync(int pageNo, int noOrders);
        Task<Order> GetOrderByIDAsync(int orderID);
        Task<Order> UpdateOrderDetailsAsync(int orderID, Order order);
        Task<Order> AddNewOrderAsync(Order order);
        Task<int?> DeleteOrderAsync(int orderID);
        Task<bool> CheckOrderExistsAsync(int orderID);



    }
}
