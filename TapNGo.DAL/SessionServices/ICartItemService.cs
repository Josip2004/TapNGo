using TapNGo.DAL.SessionModels;

namespace TapNGo.DAL.SessionServices
{
    public interface ICartItemService
    {
        List<CartItem> GetItems();
        void SaveCart(List<CartItem> cart);
        void AddItem(int itemId, string name, decimal price);
        void RemoveItem(int itemId);
    }
}
