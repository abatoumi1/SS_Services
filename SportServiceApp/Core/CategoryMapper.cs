

using SportServiceApp.Core.Commands;
using SportServiceApp.Core.Responses;
using SportServiceApp.Models;
using System.Linq;
namespace SportServiceApp.Core
{
    public static class CategoryMapper
    {
        public static CategoryResponse ToResponse(this Category cmd)
        {
            var result = new CategoryResponse
            {
               
                Name = cmd.Name,
                CategoryId = cmd.CategoryId,
                
            };
            
            return result;
        }

       
    }
}
