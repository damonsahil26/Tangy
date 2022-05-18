using Blazored.LocalStorage;
using Tangy_Common;
using TangyWeb_Client.Service.IService;
using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;
        public event Action OnChange;
        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task Decrement(ShoppingCartVM cartToDecrement)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCartVM>>(StaticData.ShoppingCart);

            //If cart item count is 0 or 1 then Remove cart Item.

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == cartToDecrement.ProductId && cart[i].ProductPriceId == cartToDecrement.ProductPriceId)
                {
                    if (cartToDecrement.Count == 0 || cart[i].Count == 1)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= cartToDecrement.Count;
                    }
                }
            }
            await _localStorageService.SetItemAsync(StaticData.ShoppingCart, cart);
            OnChange.Invoke();
        }

        public async Task Increment(ShoppingCartVM cartToAdd)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCartVM>>(StaticData.ShoppingCart);
            bool itemAlreadyExists = false;
            if (cart == null)
            {
                cart = new List<ShoppingCartVM>();
            }

            //If item already exists
            foreach (var item in cart)
            {
                if (item.ProductId == cartToAdd.ProductId && item.ProductPriceId == cartToAdd.ProductPriceId)
                {
                    itemAlreadyExists = true;
                    item.Count += cartToAdd.Count;
                }
            }
            if (!itemAlreadyExists)
            {
                cart.Add(new ShoppingCartVM()
                {
                    ProductId = cartToAdd.ProductId,
                    ProductPriceId = cartToAdd.ProductPriceId,
                    Count = cartToAdd.Count
                });
            }
            await _localStorageService.SetItemAsync(StaticData.ShoppingCart, cart);
            OnChange.Invoke();
        }
    }
}
