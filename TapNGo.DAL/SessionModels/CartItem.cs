namespace TapNGo.DAL.SessionModels
{
    public class CartItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
