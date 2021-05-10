using MediatR;
using Microsoft.EntityFrameworkCore;

using SportServiceApp.Core.Commands.Products;
using SportServiceApp.Core.Responses;
using SportServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Products
{
    public class PutProductHandler : IRequestHandler<PutProductCommand, ProductResponse>
    {
        private readonly DataContext _dbContext;
       // private readonly IOrderPlacedEventPublisher _orderPlacedEventPublisher;

        public PutProductHandler(DataContext dbContext)//, IOrderPlacedEventPublisher orderPlacedEventPublisher)
        {
            _dbContext = dbContext;
           // _orderPlacedEventPublisher = orderPlacedEventPublisher;
        }

        public async Task<ProductResponse> Handle(PutProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                                                  .Include(p => p.Supplier)
                                                  .Include(s => s.Category)
                                                  .Include(p => p.Ratings)
                                                  .FirstOrDefaultAsync(s=>s.ProductId==request.ProductId);

            if (product == null)
                throw new KeyNotFoundException($"Unable to modify order because an entry with Id: {request.ProductId} could not be found");

            product.Ratings.Clear();

            List<Rating> rates = new List<Rating>();
            foreach (var id in request.RatingIds)
            {
                var rate = await _dbContext.Ratings.FindAsync(id);
                if (rate != null)
                {
                    rates.Add(rate);
                }
            }
            product.Ratings = rates;
            product.Supplier = await _dbContext.Suppliers.FindAsync(request.SupplierId);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Category = await _dbContext.Categories.FindAsync(request.CategoryId); ;

            await _dbContext.SaveChangesAsync(cancellationToken);

            //await _orderPlacedEventPublisher.Publish(order.ToOrderPlacedEvent(quantityBeforeReduction));
            
            var response = product.ToResponse();

            return response;
        }
    }
}
