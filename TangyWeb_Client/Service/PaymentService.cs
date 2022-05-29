using Newtonsoft.Json;
using System.Text;
using Tangy_Models.DTO;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SuccessModelDTO> CheckOut(StripePaymentDTO stripePaymentDTO)
        {
            try
            {
                var content = JsonConvert.SerializeObject(stripePaymentDTO);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);

                var responseResult = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessModelDTO>(responseResult);
                    return result;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
