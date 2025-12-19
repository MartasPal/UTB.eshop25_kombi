using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UTB.eshop25.Domain.Entities
{
    public class Order : Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTimeCreated { get; protected set; }

        [Required]
        public string OrderNumber { get; set; } = String.Empty;

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public string CustomerName { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User? User { get; set; }

        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
