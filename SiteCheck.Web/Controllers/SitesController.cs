using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteCheck.Application.Sites.Commands.CreateSite;
using SiteCheck.Application.Sites.Queries.GetSites;
using SiteCheck.Web.Models.DTO;
using SiteCheck.Web.Models.ViewModels;
using System.Threading.Tasks;

namespace SiteCheck.Web.Controllers
{
    public class SitesController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SitesController(IMediator mediator, IMapper mapper) => (this.mediator,this.mapper) = (mediator,mapper);

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            var query = new GetSitesQuery() { UserName = userName };
            var vm = await mediator.Send(query);

            return View(new SitesViewModel 
            {
                UserName = userName,
                Sites = vm.Sites
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddSite(CreateSiteDto createSiteDto)
        {
            var command = mapper.Map<CreateSiteCommand>(createSiteDto);
            await mediator.Send(command);
            return RedirectToAction("Index", "Sites");
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddSite()
        {
            return View(new CreateSiteDto {UserName = HttpContext.User.Identity.Name });
        }
    }
}
