using MediatR;
using SportServiceApp.Core.Responses;
using System.Collections.Generic;

namespace SportServiceApp.Core.Commands.Products
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
       // public long ProductId { get; set; }

        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }

       
        public decimal Price { get; set; }

        public long  SupplierId { get; set; }
        public List<long> RatingIds { get; set; }

        public CreateProductCommand(string name, long categoryId, string description, decimal price, long supplierId, List<long> ratingIds)
        {
            Name = name;
            CategoryId = categoryId;
            Description = description;
            Price = price;
            SupplierId = supplierId;
            RatingIds = ratingIds;

        }
    }
}
