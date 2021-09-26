using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteCheck.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCheck.Application.Sites.Queries.GetSiteById
{
    public class GetSiteByIdQueryHandler : IRequestHandler<GetSiteByIdQuery, string>
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;

        public GetSiteByIdQueryHandler(IDbContext context, IMapper mapper) =>
            (this.context, this.mapper) = (context, mapper);

        public async Task<string> Handle(GetSiteByIdQuery request, CancellationToken cancellationToken)
        {
            var site = await context.Sites.FirstOrDefaultAsync(s => s.Id == request.SIteId);
            return site.SiteLink;
        }
    }
}
