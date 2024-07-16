using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public decimal TotalPrice => Products.Sum(p => p.Price);
    }
}
