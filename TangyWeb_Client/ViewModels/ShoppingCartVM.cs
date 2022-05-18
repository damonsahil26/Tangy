using Tangy_Models.DTO;

namespace TangyWeb_Client.ViewModels
{
    public class ShoppingCartVM
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPriceDTO ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
