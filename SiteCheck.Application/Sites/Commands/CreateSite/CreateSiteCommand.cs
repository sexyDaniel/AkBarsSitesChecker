using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCheck.Application.Sites.Commands.CreateSite
{
    public class CreateSiteCommand:IRequest
    {
        public string SiteLink { get; set; }
        public string UserName { get; set; }
        public bool IsAvailable { get; set; }
        public int SecondCount { get; set; }
    }
}
