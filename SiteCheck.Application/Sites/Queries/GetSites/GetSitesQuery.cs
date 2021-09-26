using MediatR;

namespace SiteCheck.Application.Sites.Queries.GetSites
{
    public class GetSitesQuery: IRequest<SitesVm>
    {
        public string UserName { get; set; }
    }
}
