namespace UTB.eshop25.Web.Models.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}
