using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy_Business.Repository.Interfaces;
using Tangy_Common;
using Tangy_Models.DTO;

namespace Tamgy_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderRepository.GetAllOrders());
        }

        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> GetOrderById(int? orderHeaderId)
        {
            if (orderHeaderId == 0 || orderHeaderId == null)
            {
                return BadRequest(new ErrorModelDTO
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            return Ok(await _orderRepository.GetOrderById(orderHeaderId.Value));
        }

        [HttpPost]
        [ActionName("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] StripePaymentDTO stripePaymentDTO)
        {
            var result = await _orderRepository.CreateOrder(stripePaymentDTO.Order);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeaderDTO.SessionId);

            if (sessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDTO.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModelDTO
                    {
                        ErrorMessage = "Cannot mark payment as successful."
                    });
                }
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
