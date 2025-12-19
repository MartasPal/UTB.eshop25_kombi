using System.ComponentModel.DataAnnotations;

namespace UTB.eshop25.Domain.Entities
{
    public class User : Entity<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [Required, MaxLength(200)]
        public string Address { get; set; } = "";

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(30)]
        public string? Phone { get; set; }
    }
}
