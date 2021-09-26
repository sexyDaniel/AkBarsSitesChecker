using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCheck.Domain
{
    public class Site
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string SiteLink { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsAddNow { get; set; }
        public int SecondCount { get; set; }
    }
}
