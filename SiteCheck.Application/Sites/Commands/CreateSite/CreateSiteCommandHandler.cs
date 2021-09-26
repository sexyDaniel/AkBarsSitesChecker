using MediatR;
using SiteCheck.Application.Interfaces;
using SiteCheck.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCheck.Application.Sites.Commands.CreateSite
{
    public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand>
    {
        private readonly IDbContext context;
        public CreateSiteCommandHandler(IDbContext context) => this.context = context;
        public async Task<Unit> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            var user = context.Users.FirstOrDefault(u=>u.UserName== request.UserName);
            var site = new Site()
            {
                UserId = user.Id,
                SiteLink = request.SiteLink,
                IsAvailable = request.IsAvailable,
                SecondCount = request.SecondCount,
                IsAddNow = true
            };

            await context.Sites.AddAsync(site);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
