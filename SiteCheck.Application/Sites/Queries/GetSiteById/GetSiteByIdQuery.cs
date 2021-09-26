using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCheck.Application.Sites.Queries.GetSiteById
{
    public class GetSiteByIdQuery:IRequest<string>
    {
        public int SIteId { get; set; }
    }
}
