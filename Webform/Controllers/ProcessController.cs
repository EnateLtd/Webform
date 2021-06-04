using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Webform.Models;

namespace Webform.Controllers
{
    public class ProcessController : ApiController
    {
      
        public string Post([FromBody] createticktDTO value)
        {
            helpers h = new helpers();
            var fileGUID = "";
            if (value.fileGUID != "")
            {
                fileGUID = value.fileGUID.Substring(1, value.fileGUID.Length - 2);
            }
            var apikey = h.loginToEnate();
            var userGUID = h.contactIfExists(value.userName, value.emailAddress, apikey);
            return h.createWorkItem(userGUID, value.Title, value.Description, fileGUID, value.fileName, apikey);
        }
    }



    public class helpers
    {

        public string loginToEnate()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["clientUrl"] + "Authentication/Login?useCookie=false";
            var client = new RestClient(url);
            var request = new RestRequest();
            string apikey;


            var body = new loginData
            {
                username = System.Configuration.ConfigurationManager.AppSettings["username"],
                password = System.Configuration.ConfigurationManager.AppSettings["password"]
            };

            request.AddJsonBody(body);

            var response = client.Post(request);
            apikey = response.Content;
            apikey = apikey.Replace("+", "%2B").Replace("/", "%2F").Replace("=", "%3D");
            apikey = apikey.Substring(1, apikey.Length - 2);
            return apikey;
        }

        public string createContact(string username, string email, string apikey)
        {
            var useremail = "";
            var userGUID = "";
            string url = System.Configuration.ConfigurationManager.AppSettings["clientUrl"] + "Contact/CreateContact?authToken=" + apikey;
            var client = new RestClient(url);
            var request = new RestRequest();
            var body = new createContactDTO
            {
                EmailAddress = email,
                Firstname = username
            };

            request.AddJsonBody(body);
            var response = client.Post(request);

            JavaScriptSerializer deSerializedResponse = new JavaScriptSerializer();
            createContactresponseDTO root = (createContactresponseDTO)deSerializedResponse.Deserialize(response.Content, typeof(createContactresponseDTO));


            useremail = root.Result.EmailAddress;
            userGUID = root.Result.GUID;

            return userGUID;



        }

        public string contactIfExists(string username, string email, string apikey)
        {
            var userGUID = "";
            var EmailAddress = "";


            string url = System.Configuration.ConfigurationManager.AppSettings["clientUrl"] + "Search/FreeTextContactSearch?authToken=" + apikey;
            var client = new RestClient(url);
            var request = new RestRequest();
            var body = new freeTextContactSearchDTO
            {
                Search = email
            };
            request.AddJsonBody(body);
            var response = client.Post(request);

            if (response.Content != "[]")
            {

                var json = response.Content;
                var data = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(json);
                var array = data.Select(d => d.Values.ToArray()).ToArray();

                userGUID = array[0][0].ToString();
                EmailAddress = array[0][3].ToString();


                return userGUID;
            }
            else
            {
                return createContact(username, email, apikey);

            }



        }

        public string createWorkItem(string userGUID, string title, string description, string fileGUID, string fileName, string apikey)
        {

            string url = System.Configuration.ConfigurationManager.AppSettings["clientUrl"] + "Ticket/Create?authToken=" + apikey;
            var client = new RestClient(url);
            var request = new RestRequest();

            var body = new createticktDTO
            {
                TicketAttributeVersionGUID = System.Configuration.ConfigurationManager.AppSettings["TicketAttributeVersionGUID"],
                RequesterUserGUID = userGUID,
                Title = title,
                Description = description
            };

            request.AddJsonBody(body);

            var response = client.Post(request);

            JavaScriptSerializer deSerializedResponse = new JavaScriptSerializer();
            createTicketResponse root = (createTicketResponse)deSerializedResponse.Deserialize(response.Content, typeof(createTicketResponse));
            return setToDo(root.Result.GUID, apikey, title, description, fileGUID, fileName);
        }

        public string setToDo(string packetGUID, string apikey, string title, string description, string fileGUID, string fileName)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["clientUrl"] + "Ticket/SetToDo?authToken=" + apikey;
            var client = new RestClient(url);
            var request = new RestRequest();
            List<Models.File> x = new List<Models.File>();

            if (fileGUID == "" || fileName == "")
            {
                x = new List<Models.File>();
            }
            else
            {
                x = new List<Models.File>
                {
                    new Models.File
                    {
                        TemporaryFileGUID = fileGUID,
                        FileName = fileName
                    }
                };
            };


            var body = new setToDoDTO
            {
                GUID = packetGUID,
                Title = title,
                Description = description,
                TicketCategoryAttribute =
                    new TicketCategoryAttribute {
                        GUID = System.Configuration.ConfigurationManager.AppSettings["TicketCategoryAttribute"]
                    },
                Files = x
            };

            request.AddJsonBody(body);
          
            var response = client.Post(request);

            JavaScriptSerializer deSerializedResponse = new JavaScriptSerializer();
            setToDoResponseDTO root = (setToDoResponseDTO)deSerializedResponse.Deserialize(response.Content, typeof(setToDoResponseDTO));
            return root.Result.Reference;
        }
    }

}