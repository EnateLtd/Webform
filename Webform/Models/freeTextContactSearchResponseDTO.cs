using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webform.Models
{
    public class freeTextContactSearchResponseDTO
    {
        public string UserGUID { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }
        public bool HasProfilePicture { get; set; }
        public string UserType { get; set; }
    }
}