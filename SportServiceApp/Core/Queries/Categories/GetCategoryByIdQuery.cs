using MediatR;
using SportServiceApp.Core.Responses;

namespace SportServiceApp.Core.Queries.Categories
{
    public class GetCategoryByIdQuery : IRequest<CategoryResponse>
    {
        public long ProductId { get; set; }

        public GetCategoryByIdQuery(long id)
        {
            ProductId = id;
        }
    }
}
