using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyFree.Web.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Product")]
        public string? Name { get; set; }
        [Display(Name = "Price")]
        public int Price { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        public bool isDeleted { get; set; }
        [Display(Name = "Available amount")]
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        //[Required(ErrorMessage = "Please select file")]
        //public IFormFile? Image { get; set; }
    }
}
