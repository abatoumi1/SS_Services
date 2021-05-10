using MediatR;
using SportServiceApp.Core.Responses;
using System.Collections.Generic;

namespace SportServiceApp.Core.Commands.Categories
{
    public class PutCategoryCommand : IRequest<CategoryResponse>
    {
       

        public string Name { get; set; }
        public long CategoryId { get; set; }

        public PutCategoryCommand(long categoryId, string name)
        {
           
            Name = name;
            CategoryId = categoryId;
            
        }
    }
}
