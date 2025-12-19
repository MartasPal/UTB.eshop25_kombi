using System.ComponentModel.DataAnnotations;
using UTB.eshop25.Web.Models.Cart;

namespace UTB.eshop25.Web.Models
{
    public class OrderCheckoutVM
    {
        [Required]
        public string CustomerName { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        public List<CartItem> Items { get; set; } = new();
    }
}
