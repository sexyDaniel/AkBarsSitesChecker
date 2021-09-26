using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SiteCheck.Application.Sites.Queries.GetSites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCheck.Web.Hubs
{
    public class ReloadSitesInfo:Hub
    {
        private readonly IMediator mediator;
        public ReloadSitesInfo(IMediator mediator) => this.mediator = mediator;
        public async Task Reload(string userName)
        {
            var response = await mediator.Send(new GetSitesQuery() { UserName = userName } );
            await this.Clients.All.SendAsync("Reload", response.Sites);
        }
    }
}
