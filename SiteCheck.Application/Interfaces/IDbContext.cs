using Microsoft.EntityFrameworkCore;
using SiteCheck.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCheck.Application.Interfaces
{
    public interface IDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Site> Sites { get; set; }
        DbSet<History> Histories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
