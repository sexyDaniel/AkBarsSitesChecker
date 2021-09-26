using SiteCheck.Application.Sites.Queries.GetSites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCheck.Web.Models.ViewModels
{
    public class SitesViewModel
    {
        public IList<SiteDto> Sites { get; set; }
        public string UserName { get; set; }
    }
}
