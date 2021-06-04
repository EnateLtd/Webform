using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webform.Models
{
    public class createContactDTO
    {
        public string GUID { get; set; }
        public string CompanyGUID { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime ? DateOfBirth { get; set; }
        public string Timezone { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileTelephoneNumber { get; set; }
        public string OfficeLocation { get; set; }
        public string Address { get; set; }
        public string PreferredLanguage { get; set; }
    }
}