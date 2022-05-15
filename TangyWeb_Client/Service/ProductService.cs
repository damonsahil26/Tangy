using Newtonsoft.Json;
using Tangy_Models.DTO;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string BaseServerUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync("/api/products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                foreach (var product in products)
                {
                    product.ImageUrl = BaseServerUrl + product.ImageUrl;
                }
                return products;
            }
            return new List<ProductDTO>();
        }

        public async Task<ProductDTO> GetProductById(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/products/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.ImageUrl = BaseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
