using MediatorApi.Data;
using MediatorApi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorApi.BookM
{
   
        public class List
        {
            public class Query : IRequest<List<Book>> { }

            public class Handler : IRequestHandler<Query, List<Book>>
            {
                public DataContext Context { get; }
                public Handler(DataContext context)
                {
                    Context = context;
                }
                public async Task<List<Book>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var books = await Context.Books.ToListAsync();
                    return books;
                }
            }
        }
    }

