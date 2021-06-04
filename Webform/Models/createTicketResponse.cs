using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webform.Models
{
    public class createTicketResponse
    {
        public ReferenceID Result { get; set; }
    }

    public class ReferenceID
    {

        public string Reference { get; set; }
        public string GUID { get; set; }

    }

   
   
    
}