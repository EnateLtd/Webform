using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webform.Models
{
    public class setToDoDTO
    {
        public string GUID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketCategoryAttribute TicketCategoryAttribute { get; set; }
        public List<File> Files { get; set; }
    }

    public class TicketCategoryAttribute
    {
        public string GUID { get; set; }
    }

    public class File
    {
        public string TemporaryFileGUID { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
        public string Note { get; set; }
        public string Source { get; set; }
        public bool Retired { get; set; }
        public string GUID { get; set; }
    }

}