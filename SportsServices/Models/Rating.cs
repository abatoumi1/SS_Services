using System.ComponentModel.DataAnnotations;

namespace SportsServices.Models {
    public class Rating {
        [Key]
        public long RatingId { get; set; }

        public int Stars { get; set; }

        public Product Product { get; set; }

    }
}
