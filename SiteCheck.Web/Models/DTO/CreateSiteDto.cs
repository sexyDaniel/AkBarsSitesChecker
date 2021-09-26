using AutoMapper;
using SiteCheck.Application.Common.Mapping;
using SiteCheck.Application.Sites.Commands.CreateSite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCheck.Web.Models.DTO
{
    public class CreateSiteDto:IMapWith<CreateSiteCommand>
    {
        [Required]
        public string SiteLink { get; set; }
        [Required]
        public int SecondCount { get; set; }
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSiteDto, CreateSiteCommand>()
                .ForMember(command => command.SecondCount, opt => opt.MapFrom(sDto => sDto.SecondCount))
                .ForMember(command => command.UserName, opt => opt.MapFrom(sDto => sDto.UserName))
                .ForMember(command => command.SiteLink, opt => opt.MapFrom(sDto => sDto.SiteLink));
        }
    }
}
