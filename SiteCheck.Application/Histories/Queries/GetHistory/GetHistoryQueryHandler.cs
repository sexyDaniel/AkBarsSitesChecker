using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteCheck.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCheck.Application.Histories.Queries.GetHistory
{
    public class GetHistoryQueryHandler : IRequestHandler<GetHistoryQuery, List<HistoryDto>>
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;

        public GetHistoryQueryHandler(IDbContext context, IMapper mapper) => 
            (this.context,this.mapper) = (context,mapper);

        public async Task<List<HistoryDto>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await context.Histories.Where(h => h.SiteId == request.SiteId)
                .ProjectTo<HistoryDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return history;
        }
    }
}
