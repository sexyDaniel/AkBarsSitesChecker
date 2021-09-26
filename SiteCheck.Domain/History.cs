using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCheck.Domain
{
    public class History
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
    }
}
