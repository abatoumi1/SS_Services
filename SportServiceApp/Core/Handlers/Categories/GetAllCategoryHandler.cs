using MediatR;

using Microsoft.EntityFrameworkCore;
using SportServiceApp.Core.Queries;
using SportServiceApp.Core.Queries.Categories;
using SportServiceApp.Core.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Categories
{
    public class GetAllCategoryHandler: IRequestHandler<GetAllCategoryQuery,List<CategoryResponse>>
    {
        private readonly DataContext _dbContext;

        public GetAllCategoryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Categories
                          .ToListAsync(cancellationToken: cancellationToken);

            var responses = entities.Select(x => x.ToResponse()).ToList();

            return responses;
        }
    }
}
