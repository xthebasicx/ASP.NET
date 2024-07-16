using ASP.NET.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET.ViewModel
{
    public class Productviewsmodel
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string KeyGame { get; set; }

    }
}
