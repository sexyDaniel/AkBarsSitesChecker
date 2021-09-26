using AutoMapper;
using SiteCheck.Application.Common.Mapping;
using SiteCheck.Domain;

namespace SiteCheck.Application.Sites.Queries.GetSites
{
    public class SiteDto:IMapWith<Site>
    {
        public int Id { get; set; }
        public string SiteLink { get; set; }
        public int SecondCount { get; set; }
        public bool IsAvailable { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Site, SiteDto>()
                .ForMember(sDto => sDto.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(sDto => sDto.SiteLink, opt => opt.MapFrom(s => s.SiteLink))
                .ForMember(sDto => sDto.IsAvailable, opt => opt.MapFrom(s => s.IsAvailable))
                .ForMember(sDto => sDto.SecondCount, opt => opt.MapFrom(s => s.SecondCount));
        }
    }
}
