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
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Author { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _context.Books.FindAsync(request.Id);
                if (book == null)
                {
                    throw new Exception("Could not find book");
                }
                book.Name = request.Name ?? book.Name;
                book.Author = request.Author ?? book.Author;
                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    return Unit.Value;
                }
                throw new Exception("some problem");
            }
        }
    }
}
