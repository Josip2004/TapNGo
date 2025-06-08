using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;
using TapNGo.DAL.SessionModels;

namespace TapNGo.DAL.SessionServices
{
    public class CartService : ICartItemService
    {
        private readonly IHttpContextAccessor _http;
        private const string CartKey = "Cart";

        public CartService(IHttpContextAccessor http)
        {
            _http = http;
        }

        private ISession Session => _http.HttpContext!.Session;

        public List<CartItem> GetItems()
        {
            var json = Session.GetString(CartKey);
            return string.IsNullOrEmpty(json) ?
                new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json)!;
        }

        public void SaveCart(List<CartItem> cart)
        {
            Session.SetString(CartKey, JsonSerializer.Serialize(cart));
        }

        public void AddItem(int itemId, string name, decimal price)
        {
            var cart = GetItems();

            var item = cart.FirstOrDefault(i => i.MenuItemId == itemId);

            if (item == null)
            {
                cart.Add(new CartItem() { MenuItemId = itemId, Name = name, Price = price, Quantity = 1 });   
            }
            else
            {
                item.Quantity++;
            }

            SaveCart(cart);
        }

        public void RemoveItem(int itemId)
        {
            var cart = GetItems();
            var item = cart.FirstOrDefault(i => i.MenuItemId == itemId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    cart.Remove(item);
            }
            SaveCart(cart);
        }
    }
}
