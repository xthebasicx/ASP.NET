using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET.Models
{
    public class TopUp
    {
        [Key]
        public int TopUpID { get; set; }
        [Required]
        public string Image { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
