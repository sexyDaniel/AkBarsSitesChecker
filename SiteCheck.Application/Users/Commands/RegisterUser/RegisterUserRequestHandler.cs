using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteCheck.Application.Interfaces;
using SiteCheck.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCheck.Application.Users.Commands.AuthUser
{
    public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest,bool>
    {
        private readonly IDbContext context;

        public RegisterUserRequestHandler(IDbContext context) => this.context = context;
        public async Task<bool> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user != null) 
            {
                return false;
            }

            await context.Users.AddAsync(new User
            {
                UserName = request.UserName,
                PasswordHash = request.PasswordHash
            });

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
