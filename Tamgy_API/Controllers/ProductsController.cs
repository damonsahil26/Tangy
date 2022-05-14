using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tangy_Business.Repository.Interfaces;
using Tangy_Models.DTO;

namespace Tamgy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productRepository.GetAllProducts());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid ID",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var product = await _productRepository.GetProductById(productId.Value);
            if (product == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Not Found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(product);
        }
    }
}
