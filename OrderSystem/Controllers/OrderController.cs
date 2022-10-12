using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Contracts;
using OrderSystem.Data;
using OrderSystem.Models;

namespace OrderSystem.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepo _orderRepo;

        public OrderController(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        
        [HttpGet]
        [Route("Orders/Get")]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orderList = await _orderRepo.GetOrdersAsync();
            return Ok(orderList);
        }

        [HttpGet]
        [Route("Order/Get/{orderID:int}")]
        public async Task<IActionResult> GetTaskByTaskIDAsync([FromRoute] int orderID)
        {
            var order = await _orderRepo.GetOrderByIDAsync(orderID);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        [Route("PagedOrders/Get/{pageNo:int}/{noOfOrders:int}")]
        public async Task<IActionResult> GetAllOrdersAsync(int pageNo, int noOfOrders)
        {
            var orderList = await _orderRepo.GetPagedOrdersAsync (pageNo, noOfOrders);
            if (orderList == null)
            {
                return NotFound();
            }
            return Ok(orderList);
        }

        [HttpPost]
        [Route("Order/Add")]
        public async Task<IActionResult> AddNewOrderAsync([FromBody] Order order)
        {
            var newOrder = await _orderRepo.AddNewOrderAsync(order);
            if (newOrder == null)
            {
                return NotFound();
            }
            return Ok(newOrder);
        }

        [HttpPut]
        [Route("Order/Update/{orderID:int}")]
        public async Task<IActionResult> UpdateTaskAsync([FromRoute] int orderID, [FromBody] Order order)
        {
            if (await _orderRepo.CheckOrderExistsAsync(orderID))
            {
                
                var ordertoupdate = await _orderRepo.UpdateOrderDetailsAsync(orderID, order);
                if (ordertoupdate == null)
                {
                    return NotFound();
                }
                return Ok(ordertoupdate);
                
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("Order/Cancel/{orderID:int}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int orderID)
        {
            if (await _orderRepo.CheckOrderExistsAsync(orderID))
            {
                var cancelledOrder = await _orderRepo.DeleteOrderAsync(orderID);
                return Ok(cancelledOrder);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
