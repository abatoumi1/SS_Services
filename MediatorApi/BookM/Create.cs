using MediatorApi.Data;
using MediatorApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static MediatorApi.BookM.Create;

namespace MediatorApi.BookM
{
    public class Create
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
        }
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
            var book = new Book
            {
                Id = request.Id,
                Name = request.Name,
                Author = request.Author
            };
            _context.Books.Add(book);
            var success = await _context.SaveChangesAsync() > 0;
            if (success)
            {
                return Unit.Value;
            }
            else
            {
                throw new Exception("some error");
            }

        }
    }
}
