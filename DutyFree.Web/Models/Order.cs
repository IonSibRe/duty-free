using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyFree.Web.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; } 

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
