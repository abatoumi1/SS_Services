using MediatR;
using SportServiceApp.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<CategoryResponse>
    {
        public long CategoryId { get; set; }

        public DeleteCategoryCommand(long id)
        {
            CategoryId = id;
        }
    }
}
