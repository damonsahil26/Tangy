using Newtonsoft.Json;
using System.Text;
using Tangy_Models.DTO;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Service
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string BaseServerUrl;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<OrderDTO> CreateOrder(StripePaymentDTO stripePaymentDTO)
        {
            var content = JsonConvert.SerializeObject(stripePaymentDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/order/createorder", bodyContent);
            var responseResult = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderDTO>(responseResult);
                return result;
            }

            return new OrderDTO();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders(string? userId = null)
        {
            var response = await _httpClient.GetAsync("/api/order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(content);
                return order;
            }
            return new List<OrderDTO>();
        }

        public async Task<OrderDTO> GetOrderById(int orderHeaderId)
        {
            var response = await _httpClient.GetAsync($"/api/order/{orderHeaderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDTO>(content);
                return order;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(OrderHeaderDTO orderHeaderDTO)
        {
            var content = JsonConvert.SerializeObject(orderHeaderDTO);
            var bodyContet = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/paymentsuccessful", bodyContet);

            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderHeaderDTO>(responseResult);
                return result;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
                throw new Exception(responseResult);
            }
        }
    }
}
