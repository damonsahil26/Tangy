using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.Interfaces;
using Tangy_Common;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_DataAccess.ViewModel;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO objOrderDTO)
        {
            try
            {
                var order = _mapper.Map<OrderDTO, Order>(objOrderDTO);
                _applicationDbContext.OrderHeaders.Add(order.OrderHeader);
                await _applicationDbContext.SaveChangesAsync();
                foreach (var detail in order.OrderDetail)
                {
                    detail.OrderHeaderId = order.OrderHeader.Id;
                }
                _applicationDbContext.OrderDetails.AddRange(order.OrderDetail);
                await _applicationDbContext.SaveChangesAsync();

                return new OrderDTO
                {
                    OrderDetail = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(order.OrderDetail).ToList(),
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(order.OrderHeader),
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return objOrderDTO;
        }

        public async Task<int> Delete(int id)
        {
            var order = await _applicationDbContext.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if (order != null)
            {
                var orderDetails = _applicationDbContext.OrderDetails.Where(x => x.OrderHeaderId == id);
                _applicationDbContext.OrderDetails.RemoveRange(orderDetails);
                _applicationDbContext.OrderHeaders.Remove(order);
                return _applicationDbContext.SaveChanges();
            }
            return 0;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders(string? userId = null, string? status = null)
        {
            List<Order> ordersFromDb = new List<Order>();
            IEnumerable<OrderDetail> orderDetails = _applicationDbContext.OrderDetails;
            IEnumerable<OrderHeader> orderHeaders = _applicationDbContext.OrderHeaders;

            foreach (var header in orderHeaders)
            {
                Order order = new Order
                {
                    OrderHeader = header,
                    OrderDetail = orderDetails.Where(x => x.OrderHeaderId == header.Id)
                };
                ordersFromDb.Add(order);
            }
            //Order filtering #TODO

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(ordersFromDb);
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            var order = new Order
            {
                OrderHeader = _applicationDbContext.OrderHeaders.FirstOrDefault(x => x.Id == id),
                OrderDetail = _applicationDbContext.OrderDetails.Where(x => x.OrderHeaderId == id),
            };
            if (order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }
            return new OrderDTO();
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id)
        {
            var data = await _applicationDbContext.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return new OrderHeaderDTO();
            }
            if (data.Status == StaticData.Order_Pending)
            {
                data.Status = StaticData.Order_Confirmed;
                await _applicationDbContext.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
            }
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateOrderHeader(OrderHeaderDTO objOrderHeader)
        {
            if (objOrderHeader != null)
            {
                var header = _mapper.Map<OrderHeaderDTO, OrderHeader>(objOrderHeader);
                _applicationDbContext.OrderHeaders.Update(header);
                await _applicationDbContext.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(header);
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _applicationDbContext.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }
            data.Status = status;
            if (data.Status == StaticData.Order_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
