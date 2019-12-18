using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AssistantApi_Project.Controllers.CommonProcess
{
    public class HttpData
    {



        public string StringFromUrl(string link_url)
        {


            string chuoi = "";

            using (WebClient client = new WebClient())
            {

                client.Encoding = Encoding.UTF8;

                chuoi = client.DownloadString(link_url);
            }

            return chuoi;

        }


    }
}