using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportServiceApp.Models
{
    public class Category
    {
        [Key]
        public long CategoryId { get; set; }

        public string Name { get; set; }
        public List<Product> Ratings { get; set; }
    }
}
