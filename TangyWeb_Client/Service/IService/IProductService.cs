using Tangy_Models.DTO;

namespace TangyWeb_Client.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(int productId);
    }
}
