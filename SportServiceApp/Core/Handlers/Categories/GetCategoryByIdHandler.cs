using MediatR;
using Microsoft.EntityFrameworkCore;
using SportServiceApp.Core.Queries;
using SportServiceApp.Core.Queries.Categories;
using SportServiceApp.Core.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Categories
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly DataContext _dbContext;

        public GetCategoryByIdHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Categories
                          .FirstOrDefaultAsync(s=>s.CategoryId==request.ProductId);
            var response = entity?.ToResponse();
            return response;
        }
    }
}
