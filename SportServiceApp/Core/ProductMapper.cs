

using SportServiceApp.Core.Commands;
using SportServiceApp.Core.Responses;
using SportServiceApp.Models;
using System.Linq;
namespace SportServiceApp.Core
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product cmd)
        {
            var result = new ProductResponse
            {
                ProductId = cmd.ProductId,
                Name = cmd.Name,
                CategoryId = cmd.Category.CategoryId,
                Description = cmd.Description,
                Price = cmd.Price,
                SupplierId = cmd.Supplier.SupplierId,
                RatingIds = cmd.Ratings.Select(s => s.RatingId).ToList()
            };
            
            return result;
        }

       
    }
}
