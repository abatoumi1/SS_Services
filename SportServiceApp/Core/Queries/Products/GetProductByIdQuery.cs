using MediatR;
using SportServiceApp.Core.Responses;

namespace SportServiceApp.Core.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public long ProductId { get; set; }

        public GetProductByIdQuery(long id)
        {
            ProductId = id;
        }
    }
}
