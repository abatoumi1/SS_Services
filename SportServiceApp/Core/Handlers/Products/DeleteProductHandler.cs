using MediatR;
using SportServiceApp.Core.Commands.Categories;
using SportServiceApp.Core.Commands.Products;
using SportServiceApp.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Products
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ProductResponse>
    {
        private readonly DataContext _dbContext;

        public DeleteProductHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FindAsync(request.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Unable to modify order because an entry with Id: {request.ProductId} could not be found");
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var response = product?.ToResponse();
            return response;
        }
    }
}