using MediatorApi.Data;
using MediatorApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorApi.BookM
{
    public class Detail
    {
        public class Query : IRequest<Book>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Book>
        {
            public DataContext Context { get; }
            public Handler(DataContext context)
            {
                Context = context;
            }
            public async Task<Book> Handle(Query request, CancellationToken cancellationToken)
            {
                var book = await Context.Books.FindAsync(request.Id);
                return book;
            }
        }
    }
}
