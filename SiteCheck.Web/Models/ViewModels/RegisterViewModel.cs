using AutoMapper;
using SiteCheck.Application.Common.Mapping;
using SiteCheck.Application.Users.Commands.AuthUser;
using System.ComponentModel.DataAnnotations;


namespace SiteCheck.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
