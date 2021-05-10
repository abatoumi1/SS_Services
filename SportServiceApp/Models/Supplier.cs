using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportServiceApp.Models
{
    public class Supplier {
        [Key]
        public long SupplierId { get; set; }
        
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
