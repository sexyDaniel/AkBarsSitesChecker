using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteCheck.Application.Histories.Queries.GetHistory;
using SiteCheck.Application.Sites.Queries.GetSiteById;
using SiteCheck.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCheck.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public HistoryController(IMediator mediator, IMapper mapper) => (this.mediator, this.mapper) = (mediator, mapper);

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(int siteId)
        {
            var request = new GetSiteByIdQuery() { SIteId = siteId };
            var siteLink = await mediator.Send(request);
            var history = await mediator.Send(new GetHistoryQuery {SiteId = siteId });

            return View(new HistoryViewModel 
            {
                SiteLink = siteLink,
                History = history,
                SiteId = siteId 
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Filter(HistoryViewModel viewModel) 
        {
            viewModel.History = await mediator.Send(new GetHistoryQuery { SiteId = viewModel.SiteId });
            viewModel.History = viewModel.History
                .Where(h => h.Date > viewModel.StartDate && h.Date < viewModel.EndDate)
                .ToList();

            return View("Index",viewModel);
        }
    }
}
