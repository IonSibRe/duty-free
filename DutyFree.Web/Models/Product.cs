using System.ComponentModel.DataAnnotations;

namespace DutyFree.Web.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        public bool isDeleted { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int  Quantity { get; set; }
        public string ImageUrl { get; set; }
        //public IFormFile Image { get; set; }

    }
}
