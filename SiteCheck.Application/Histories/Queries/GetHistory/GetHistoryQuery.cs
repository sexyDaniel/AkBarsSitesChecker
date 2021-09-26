using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCheck.Application.Histories.Queries.GetHistory
{
    public class GetHistoryQuery : IRequest<List<HistoryDto>>
    {
        public int SiteId { get; set; }
    }
}
