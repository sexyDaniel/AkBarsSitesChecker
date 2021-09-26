using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteCheck.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCheck.Application.Users.Commands.LoginUser
{
    public class LoginUserRequestHandler:IRequestHandler<LoginUserRequest,bool>
    {
        public readonly IDbContext context;
        public LoginUserRequestHandler(IDbContext context) => this.context = context;
        public async Task<bool> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.PasswordHash == request.PasswordHash && u.UserName == u.UserName);

            return user != null;
        }

    }
}
