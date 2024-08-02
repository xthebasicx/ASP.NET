using ASP.NET.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET.ViewModel
{
    public class TopUpviewsmodel
    {
        [Key]
        public int TopUpID { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
