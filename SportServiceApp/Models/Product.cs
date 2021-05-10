using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportServiceApp.Models
{
    public class Product {
        [Key]
        public long ProductId { get; set; }
        
        public string Name { get; set; }
        //public string Category { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
