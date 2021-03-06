﻿using MediatR;

using Microsoft.EntityFrameworkCore;
using SportServiceApp.Core.Queries;
using SportServiceApp.Core.Queries.Products;
using SportServiceApp.Core.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportServiceApp.Core.Handlers.Products
{
    public class GetAllProductHandler: IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly DataContext _dbContext;

        public GetAllProductHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Products
                          .Include(p => p.Supplier)
                          .Include(p=>p.Category)
                          .Include(p => p.Ratings)
                          .ToListAsync(cancellationToken: cancellationToken);

            var responses = entities.Select(x => x.ToResponse()).ToList();

            return responses;
        }
    }
}
