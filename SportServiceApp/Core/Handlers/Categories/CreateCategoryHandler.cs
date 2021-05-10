using MediatR;
using SportServiceApp.Core.Commands.Categories;
using SportServiceApp.Core.Responses;
using SportServiceApp.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
namespace SportServiceApp.Core.Handlers.Categories
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly DataContext _dbContext;
       // private readonly IOrderCreatedEventPublisher _createdEventPublisher;

        public CreateCategoryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
          //  _createdEventPublisher = createdEventPublisher;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand req, CancellationToken cancellationToken)
        {
            var entity =  new Category
            {               
                Name = req.Name,               
            };
           
            _dbContext.Categories.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = entity.ToResponse();

           // await _createdEventPublisher.Publish(entity.ToCreatedEvent());

            return response;
        }
    }
}
