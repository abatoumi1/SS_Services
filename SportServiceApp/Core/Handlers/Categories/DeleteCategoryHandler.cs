using MediatR;
using SportServiceApp.Core.Commands.Categories;
using SportServiceApp.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Categories
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, CategoryResponse>
    {
        private readonly DataContext _dbContext;

        public DeleteCategoryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var cat = await _dbContext.Categories.FindAsync(request.CategoryId);
            if (cat == null)
                throw new KeyNotFoundException($"Unable to modify order because an entry with Id: {request.CategoryId} could not be found");
            _dbContext.Categories.Remove(cat);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var response = cat?.ToResponse();
            return response;
        }
    }
}