using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.Interfaces;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductPriceRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductPriceDTO> CreateProductPrice(ProductPriceDTO ProductPriceDTO)
        {
            var ProductPrice = _mapper.Map<ProductPriceDTO, ProductPrice>(ProductPriceDTO);
            var addedObj = _dbContext.ProductPrices.Add(ProductPrice);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductPrice, ProductPriceDTO>(addedObj.Entity);
        }

        public async Task<int> DeleteProductPrice(int Id)
        {
            var ProductPrice = await _dbContext.ProductPrices.FirstOrDefaultAsync(x => x.Id == Id);
            if (ProductPrice != null)
            {
                var deletedObj = _dbContext.ProductPrices.Remove(ProductPrice);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAllProductPrices(int ProductId)
        {
            return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_dbContext.ProductPrices.Where(x => x.ProductId == ProductId));
        }

        public async Task<ProductPriceDTO> GetProductPriceById(int Id)
        {
            var ProductPrice = await _dbContext.ProductPrices.FirstOrDefaultAsync(x => x.Id == Id);
            if (ProductPrice != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(ProductPrice);
            }
            return new ProductPriceDTO();
        }

        public async Task<ProductPriceDTO> UpdateProductPrice(ProductPriceDTO ProductPriceDTO)
        {
            var ProductPrice = await _dbContext.ProductPrices.FirstOrDefaultAsync(x => x.Id == ProductPriceDTO.Id);
            if (ProductPrice != null)
            {
                ProductPrice.ProductId = ProductPriceDTO.ProductId;
                ProductPrice.Size = ProductPriceDTO.Size;
                ProductPrice.Price = ProductPriceDTO.Price;
                _dbContext.ProductPrices.Update(ProductPrice);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(ProductPrice);
            }

            return new ProductPriceDTO();
        }
    }
}
