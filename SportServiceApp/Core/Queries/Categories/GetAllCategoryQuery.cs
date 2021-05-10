using System.Collections.Generic;
using MediatR;
using SportServiceApp.Core.Responses;

namespace SportServiceApp.Core.Queries.Categories
{
    public class GetAllCategoryQuery: IRequest<List<CategoryResponse>>
    {
    }
}
