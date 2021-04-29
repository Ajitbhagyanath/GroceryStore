using System.ComponentModel.DataAnnotations;
namespace GroceryStoreAPI.Domain.Model
{
    public class Customer
    {
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
    }
   
}