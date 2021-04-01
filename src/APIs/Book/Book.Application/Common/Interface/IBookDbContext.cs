using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System.Threading;
using System.Threading.Tasks;

namespace Book.Application.Common.Interface
{
    public interface IBookDbContext
    {
        DbSet<Domain.Entities.Author> Authors { get; set; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
