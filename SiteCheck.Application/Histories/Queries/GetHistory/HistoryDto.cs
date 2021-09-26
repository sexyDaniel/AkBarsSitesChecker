using AutoMapper;
using SiteCheck.Application.Common.Mapping;
using SiteCheck.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCheck.Application.Histories.Queries.GetHistory
{
    public class HistoryDto:IMapWith<History>
    {
        public DateTime Date { get; set; }
        public bool IsAvaliable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<History, HistoryDto>()
                .ForMember(hDto => hDto.Date, opt => opt.MapFrom(h => h.Date))
                .ForMember(hDto => hDto.IsAvaliable, opt => opt.MapFrom(h => h.IsAvailable));
        }
    }
}
