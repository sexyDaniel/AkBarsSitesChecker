using MediatR;

namespace SiteCheck.Application.Users.Commands.AuthUser
{
    public class RegisterUserRequest:IRequest<bool>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
