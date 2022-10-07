using System.ComponentModel.DataAnnotations;

namespace DutyFree.Web.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public int Role { get; set; }
    }
}
