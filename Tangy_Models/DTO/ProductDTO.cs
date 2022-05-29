using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Description { get; set; } = String.Empty;
        public bool ShopFav { get; set; }
        public bool CustomerFav { get; set; }
        public string Color { get; set; } = String.Empty;
        public string ImageUrl { get; set; } = String.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Please select a categoy")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public ICollection<ProductPriceDTO> Prices { get; set; }
    }
}
