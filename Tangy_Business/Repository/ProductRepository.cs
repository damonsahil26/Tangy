using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tangy_Business.Repository.Interfaces;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(productDTO);
            var addedObj = _dbContext.Add<Product>(product);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(addedObj.Entity);
        }

        public async Task<int> DeleteProduct(int Id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_dbContext.Products.Include(u => u.Category));
        }

        public async Task<ProductDTO> GetProductById(int Id)
        {
            var product = await _dbContext.Products.Include(u => u.Category).FirstOrDefaultAsync(x => x.Id == Id);
            if (product != null)
            {
                return _mapper.Map<Product, ProductDTO>(product);
            }
            return new ProductDTO();
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productDTO.Id);
            if (product != null)
            {
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.CustomerFav = productDTO.CustomerFav;
                product.ShopFav = productDTO.ShopFav;
                product.Color = productDTO.Color;
                product.ImageUrl = productDTO.ImageUrl;
                product.CategoryId = productDTO.CategoryId;
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(product);
            }
            return productDTO;
        }
    }
}
