using AssistantApi_Project.Controllers.FeatureProcess;
using AssistantApi_Project.Controllers.CommonProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;

namespace AssistantApi_Project.Controllers
{
    public class ResponseController : ApiController
    {
        public static string stringRequest = "";
        public static string keyworld_wiki = "";
        public List<string> lstChuoi = new List<string>();

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

                //StringAnalysis stringanalysis = new StringAnalysis();
                SearchInfo searchinfo = new SearchInfo();



                string keyworld = searchinfo.SimplifyStringForWiki(request);
                keyworld_wiki = keyworld;

                System.Diagnostics.Debug.WriteLine(keyworld);

                List<string> lstKeyworld = new List<string>();
                lstKeyworld = searchinfo.SeparateString(keyworld_wiki);

                for (int i = 0; i < lstKeyworld.Count; i++)
                {

                    System.Diagnostics.Debug.WriteLine("List Key: " + lstKeyworld[i]);
                  
                    
                    SearchInfo searchInfo = new SearchInfo();
                    string str_contend = searchInfo.GetWikiInfomationsUrl("https://vi.m.wikipedia.org/w/api.php?action=opensearch&search=" + lstKeyworld[i] + "&limit=1&format=xml");
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


            if (request_type.Equals("media"))
            {
                MediaPlayer media = new MediaPlayer();


                string test = media.SimplifyStringForMedia(request);

                System.Diagnostics.Debug.WriteLine("Media:" + test);

                media.getLinkMedia(test);

                resulter.Contend = media.getLinkMedia(test);

                resulter.Type = "Media";

            }

            if (request_type.Equals("reminder"))
            {
                Reminder reminder = new Reminder();

               // System.Diagnostics.Debug.WriteLine(reminder.getTimer("tôi muốn đặt lịch hẹn đi ăn nhậu vào thứ 5 tuần này lúc 6 giờ kém 20 tối"));

                resulter.Contend = reminder.getTimer(request);

                resulter.Type = reminder.getContend();

            }

            if (request_type.Equals("none"))
            {

                resulter.Contend = "không thể thực hiện yêu cầu";
                resulter.Type = "None";

            }

    
            yield return resulter;
        }

     



        // POST api/values
        public void Post([FromBody]string value)
        {
        }


    }
}
