using MediatR;
using Microsoft.EntityFrameworkCore;
using SportServiceApp.Core.Commands;
using SportServiceApp.Core.Commands.Categories;
using SportServiceApp.Core.Responses;
using SportServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Categories
{
    public class PutCategoryHandler : IRequestHandler<PutCategoryCommand, CategoryResponse>
    {
        private readonly DataContext _dbContext;
       // private readonly IOrderPlacedEventPublisher _orderPlacedEventPublisher;

        public PutCategoryHandler(DataContext dbContext)//, IOrderPlacedEventPublisher orderPlacedEventPublisher)
        {
            _dbContext = dbContext;
           // _orderPlacedEventPublisher = orderPlacedEventPublisher;
        }

        public async Task<CategoryResponse> Handle(PutCategoryCommand request, CancellationToken cancellationToken)
        {
            var cat = await _dbContext.Categories
                                                  .FirstOrDefaultAsync(s=>s.CategoryId==request.CategoryId);

            if (cat == null)
                throw new KeyNotFoundException($"Unable to modify order because an entry with Id: {request.CategoryId} could not be found");

          
           
            cat.Name = request.Name;
            

            await _dbContext.SaveChangesAsync(cancellationToken);

            //await _orderPlacedEventPublisher.Publish(order.ToOrderPlacedEvent(quantityBeforeReduction));
            
            var response = cat.ToResponse();

            return response;
        }
    }
}
