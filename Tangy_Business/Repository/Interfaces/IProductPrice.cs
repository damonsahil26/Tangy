using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IProductPriceRepository
    {
        public Task<ProductPriceDTO> CreateProductPrice(ProductPriceDTO ProductPriceDTO);
        public Task<ProductPriceDTO> UpdateProductPrice(ProductPriceDTO ProductPriceDTO);
        public Task<int> DeleteProductPrice(int Id);
        public Task<IEnumerable<ProductPriceDTO>> GetAllProductPrices(int Id);
        public Task<ProductPriceDTO> GetProductPriceById(int Id);
    }
}
