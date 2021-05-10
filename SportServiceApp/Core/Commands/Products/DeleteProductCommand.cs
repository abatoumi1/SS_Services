using MediatR;
using SportServiceApp.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Commands.Products
{
    public class DeleteProductCommand : IRequest<ProductResponse>
    {
        public long ProductId { get; set; }

        public DeleteProductCommand(long id)
        {
            ProductId = id;
        }
    }
}
