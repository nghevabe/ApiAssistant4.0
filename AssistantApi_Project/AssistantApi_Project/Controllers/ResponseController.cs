using AssistantApi_Project.Controllers.FeatureProcess;
using AssistantApi_Project.Controllers.CommonProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AssistantApi_Project.Controllers
{
    public class ResponseController : ApiController
    {
        public static string stringRequest = "";
        public static string keyworld_wiki = "";

        [System.Web.Http.Route("api/assistant")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Models.Resulter> GetResponse(string request)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Models.Resulter resulter = new Models.Resulter();

            stringRequest = request;


            Devider devider = new Devider();
        
            string request_type = devider.RequestDevider(request);
        

            if (request_type.Equals("wiki"))
            {

                StringAnalysis stringanalysis = new StringAnalysis();
                
                
                string keyworld = stringanalysis.SimplifyString(request);
                keyworld_wiki = keyworld;

                System.Diagnostics.Debug.WriteLine(keyworld);
          
                List<string> lstKeyworld = new List<string>();
                lstKeyworld = stringanalysis.SeparateString(request);
               
                for (int i = 0; i < lstKeyworld.Count; i++)
                {
               
                    System.Diagnostics.Debug.WriteLine("List Key: " + lstKeyworld[i]);

                    SearchInfo searchInfo = new SearchInfo();
                    string str_contend = searchInfo.GetWikiInfomations("https://vi.m.wikipedia.org/w/api.php?action=opensearch&search=" + lstKeyworld[i] + "&limit=1&format=xml");
                    if(str_contend.Equals("không có kết quả"))
                    {

                        resulter.Contend = "không tìm thấy kết quả";
                        resulter.Type = "None";

                    }

                    if (!str_contend.Equals("không có kết quả"))
                    {

                        resulter.Contend = str_contend;
                        resulter.Type = "Wiki";
                        break;

                    }

                 
                }

            }

            if (request_type.Equals("weather"))
            {
                Weather weather = new Weather();
                resulter.Contend = weather.getWeatherResponse(request);

                resulter.Type = "Weather";
             
            }

            if (request_type.Equals("none"))
            {
              
                resulter.Contend = "không thể thực hiện yêu cầu";
                resulter.Type = "None";

            }


            yield return resulter;
        }

        public void GetWiki(string request)
        {
            StringAnalysis stringanalysis = new StringAnalysis();
            List<string> lstKeyworld = new List<string>();
            lstKeyworld = stringanalysis.SeparateString(request);

            for(int i = 0; i < lstKeyworld.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("List Key: "+lstKeyworld[i]);
            }

        }



        // POST api/values
        public void Post([FromBody]string value)
        {
        }


    }
}
