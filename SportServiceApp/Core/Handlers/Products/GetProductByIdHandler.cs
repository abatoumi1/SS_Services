using MediatR;
using Microsoft.EntityFrameworkCore;

using SportServiceApp.Core.Queries.Products;
using SportServiceApp.Core.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly DataContext _dbContext;

        public GetProductByIdHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products
                          .Include(p => p.Supplier)
                          .Include(s => s.Category)
                          .Include(p => p.Ratings)
                          .FirstOrDefaultAsync(s=>s.ProductId==request.ProductId);
            var response = entity?.ToResponse();
            return response;
        }
    }
}
