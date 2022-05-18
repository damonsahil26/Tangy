using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Service.IService
{
    public interface ICartService
    {
        event Action OnChange;
        Task Increment(ShoppingCartVM shoppingCart);
        Task Decrement(ShoppingCartVM shoppingCart);
    }
}
