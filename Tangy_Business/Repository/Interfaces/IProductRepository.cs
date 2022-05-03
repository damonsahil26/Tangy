using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task<ProductDTO> CreateProduct(ProductDTO productDTO);
        public Task<ProductDTO> UpdateProduct(ProductDTO productDTO);
        public Task<int> DeleteProduct(int Id);
        public Task<IEnumerable<ProductDTO>> GetAllProducts();
        public Task<ProductDTO> GetProductById(int Id);
    }
}
