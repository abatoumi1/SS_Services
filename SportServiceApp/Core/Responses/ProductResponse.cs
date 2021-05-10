using System.Collections.Generic;

namespace SportServiceApp.Core.Responses
{
    public class ProductResponse
    {
        public ProductResponse()
        {
            RatingIds = new  List<long>();

        }
        public long ProductId { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }


        public decimal Price { get; set; }

        public long SupplierId { get; set; }
        public List<long> RatingIds { get; set; }
    }
}
