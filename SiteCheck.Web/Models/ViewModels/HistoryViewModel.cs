using SiteCheck.Application.Histories.Queries.GetHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCheck.Web.Models.ViewModels
{
    public class HistoryViewModel
    {
        public int SiteId { get; set; }
        public string SiteLink { get; set; }
        public List<HistoryDto> History {get;set;}
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
