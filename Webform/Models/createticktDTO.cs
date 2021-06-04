using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webform.Models
{
    public class createticktDTO
    {
        public string TicketAttributeVersionGUID { get; set; }
        public string RequesterUserGUID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string emailAddress { get; set; }
        public string userName { get; set; }
        public string fileName { get; set; }
        public string fileGUID { get; set; }

    }
}