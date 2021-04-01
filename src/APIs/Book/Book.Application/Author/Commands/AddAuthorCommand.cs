using Book.Application.Author.Queries;
using Book.Application.Common.Interface;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Book.Application.Author.Commands
{
    public class AddAuthorCommand : IRequest<AuthorDTO>
    {
        public string Name { get; set; }
    }

    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AuthorDTO>
    {
        private readonly IBookDbContext _context;

        public AddAuthorCommandHandler(IBookDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorDTO> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Domain.Entities.Author(request.Name);

            _context.Authors.Add(author);

            await _context.SaveChangesAsync(cancellationToken);

            return new AuthorDTO
            {
                Id = author.Id.ToString(),
                Name = author.Name
            };
        }
    }
}
