using MediatR;

namespace SiteCheck.Application.Users.Commands.LoginUser
{
    public class LoginUserRequest:IRequest<bool>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
