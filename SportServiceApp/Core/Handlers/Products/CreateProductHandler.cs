using MediatR;
using SportServiceApp.Core.Commands;
using SportServiceApp.Core.Responses;
using SportServiceApp.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using SportServiceApp.Core.Commands.Products;

namespace SportServiceApp.Core.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly DataContext _dbContext;
       // private readonly IOrderCreatedEventPublisher _createdEventPublisher;

        public CreateProductHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
          //  _createdEventPublisher = createdEventPublisher;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand req, CancellationToken cancellationToken)
        {
            var entity =  new Product
            {
                
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
            };
            entity.Supplier = await _dbContext.Suppliers.FindAsync(req.SupplierId);
            entity.Category = await _dbContext.Categories.FindAsync(req.CategoryId);
            List<Rating> rates = new List<Rating>();
            foreach(var id in req.RatingIds)
            {
                var rate = await _dbContext.Ratings.FindAsync(id);
                if (rate != null)
                {
                    rates.Add(rate);
                }
            }
            entity.Ratings = rates;
            _dbContext.Products.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = entity.ToResponse();

           // await _createdEventPublisher.Publish(entity.ToCreatedEvent());

            return response;
        }
    }
}
