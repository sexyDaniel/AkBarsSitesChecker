using SiteCheck.Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SiteCheck.Application.Sites.Queries.GetSites
{
    public class GetSitesQueryHandler:IRequestHandler<GetSitesQuery,SitesVm>
    {
        private readonly IDbContext context;

        public GetSitesQueryHandler(IDbContext context) => 
            (this.context) = (context);

        public async Task<SitesVm> Handle(GetSitesQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user is null)
            {
                return new SitesVm { Sites = [] };
            }

            var sites = await context.Sites.Where(s => s.UserId == user.Id)
                .Select(x => new SiteDto()
                {
                    Id = x.Id,
                    IsAvailable = x.IsAvailable,
                    SecondCount = x.SecondCount,
                    SiteLink = x.SiteLink,
                })
                .ToListAsync();

            return new SitesVm { Sites= sites };
        }
    }
}
