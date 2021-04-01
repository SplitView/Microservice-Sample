using Book.Application.Common.Interface;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Book.Application.Author.Queries
{
    public class GetAllAuthors : IRequest<List<AuthorDTO>>
    {

    }

    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthors, List<AuthorDTO>>
    {
        private readonly IBookDbContext _context;

        public GetAllAuthorsHandler(IBookDbContext context)
        {
            _context = context;
        }


        public async Task<List<AuthorDTO>> Handle(GetAllAuthors request, CancellationToken cancellationToken)
        {
            var authors = await _context.Authors.Select(x => new AuthorDTO
            {
                Id = x.Id.ToString(),
                Name = x.Name
            })
                .ToListAsync();

            return authors;
        }
    }

}
