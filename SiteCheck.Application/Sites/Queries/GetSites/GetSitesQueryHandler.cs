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
        private readonly IMapper mapper;

        public GetSitesQueryHandler(IDbContext context, IMapper mapper) => 
            (this.context,this.mapper) = (context,mapper);

        public async Task<SitesVm> Handle(GetSitesQuery request, CancellationToken cancellationToken)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == request.UserName);
            var sites = await context.Sites.Where(s => s.UserId == user.Id)
                .ProjectTo<SiteDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new SitesVm { Sites= sites };
        }
    }
}
