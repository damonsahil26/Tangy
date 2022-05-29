using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> GetOrderById(int id);
        public Task<IEnumerable<OrderDTO>> GetAllOrders(string? userId = null, string? status = null);
        public Task<OrderDTO> CreateOrder(OrderDTO objOrderDTO);
        public Task<int> Delete(int id);
        public Task<OrderHeaderDTO> UpdateOrderHeader(OrderHeaderDTO objOrderHeader);
        public Task<OrderHeaderDTO> MarkPaymentSuccessful(int id);
        public Task<bool> UpdateOrderStatus(int orderId, string status);
        public Task<OrderHeaderDTO> CancelOrder(int orderId);
    }
}
