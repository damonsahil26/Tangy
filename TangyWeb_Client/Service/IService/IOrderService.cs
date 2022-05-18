using Tangy_Models.DTO;

namespace TangyWeb_Client.Service.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrders(string? userId);
        Task<OrderDTO> GetOrderById(int orderHeaderId);
    }
}
