using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Webform.Controllers
{
    public class PreapareNewFileController : ApiController
    {
        public string Post()
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            var tempFileGuid = "";
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile file = hfc[iCnt];
                var client = new HttpClient();
                var content = new MultipartFormDataContent();

                byte[] Bytes = new byte[file.InputStream.Length + 1];
                file.InputStream.Read(Bytes, 0, Bytes.Length);
                var fileContent = new ByteArrayContent(Bytes);
                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                content.Add(fileContent);
                var requestUri = System.Configuration.ConfigurationManager.AppSettings["clientUrl"] + "Packet/PrepareNewFile?authToken=" + new helpers().loginToEnate();
                var result = client.PostAsync(requestUri, content).Result;

                tempFileGuid = result.Content.ReadAsStringAsync().Result;

            }
            return tempFileGuid;
        }

    }
}