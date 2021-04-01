using Book.Application.Common.Interface;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Book.Application.Author.Queries
{
    public class GetAuthorById : IRequest<AuthorDTO>
    {
        public GetAuthorById(Guid id)
        {
            AuthorId = id;
        }

        public Guid AuthorId { get; set; }
    }

    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorById, AuthorDTO>
    {
        private readonly IBookDbContext _context;

        public GetAuthorByIdHandler(IBookDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorDTO> Handle(GetAuthorById request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.AuthorId);

            return new AuthorDTO
            {
                Id = author.Id.ToString(),
                Name = author.Name
            };
        }
    }
}
