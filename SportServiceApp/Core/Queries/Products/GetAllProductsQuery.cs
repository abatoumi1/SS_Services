using System.Collections.Generic;
using MediatR;
using SportServiceApp.Core.Responses;

namespace SportServiceApp.Core.Queries.Products
{
    public class GetAllProductsQuery: IRequest<List<ProductResponse>>
    {
    }
}
