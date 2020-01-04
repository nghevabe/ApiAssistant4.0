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


                System.Diagnostics.Debug.WriteLine("Yes:" );
                string keyworld = searchinfo.SimplifyStringForWiki(request);
                keyworld_wiki = keyworld;

                System.Diagnostics.Debug.WriteLine("|"+keyworld+"|");

                List<string> lstKeyworld = new List<string>();
                lstKeyworld = searchinfo.SeparateString(keyworld_wiki);

                string[] keyworld_cut = keyworld.Split(' ');
                if(keyworld_cut.Length == 2)
                {

                    SearchInfo searchInfo = new SearchInfo();
               
                    string str_contend = searchInfo.GetWikiInfomationsUrl("https://vi.m.wikipedia.org/w/api.php?action=opensearch&search=" + keyworld + "&limit=1&format=xml");
                    resulter.Contend = str_contend;
                    System.Diagnostics.Debug.WriteLine("Có Ket Qua 0: " + str_contend);
                    resulter.Type = "Wiki";
                    resulter.Title = "None";
                    resulter.Timer = "None";
                }

                for (int i = 0; i < lstKeyworld.Count; i++)
                {

                    System.Diagnostics.Debug.WriteLine("List Key: " + lstKeyworld[i]);
                  
                    
                    SearchInfo searchInfo = new SearchInfo();
                    System.Diagnostics.Debug.WriteLine("world: " + lstKeyworld[i]);
                    string str_contend = searchInfo.GetWikiInfomationsUrl("https://vi.m.wikipedia.org/w/api.php?action=opensearch&search=" + lstKeyworld[i] + "&limit=1&format=xml");
                    if(str_contend.Equals("không có kết quả"))
                    {

                        resulter.Contend = "không tìm thấy kết quả";
                        resulter.Type = "None";
                        resulter.Title = "None";
                        resulter.Timer = "None";


                    }

                    if (!str_contend.Equals("không có kết quả"))
                    {

                        resulter.Contend = str_contend;
                        System.Diagnostics.Debug.WriteLine("Có Ket Qua: "+str_contend);
                        resulter.Type = "Wiki";
                        resulter.Title = "None";
                        resulter.Timer = "None";
                        break;

                    }
                    

                }

            }

            if (request_type.Equals("weather"))
            {
                Weather weather = new Weather();
                resulter.Contend = weather.getWeatherResponse(request);
                resulter.Timer = "None";
                resulter.Title = "None";
                resulter.Type = "Weather";

            }


            if (request_type.Equals("media"))
            {
                MediaPlayer media = new MediaPlayer();


                string title = media.SimplifyStringForMedia(request);

                System.Diagnostics.Debug.WriteLine("Media:" + title);

                media.getLinkMedia(title);

                resulter.Contend = media.getLinkMedia(title);
                resulter.Timer = "None";
                resulter.Title = title;
                resulter.Type = "Media";

            }

            if (request_type.Equals("reminder"))
            {
                Reminder reminder = new Reminder();

                // System.Diagnostics.Debug.WriteLine(reminder.getTimer("tôi muốn đặt lịch hẹn đi ăn nhậu vào thứ 5 tuần này lúc 6 giờ kém 20 tối"));
                
                //System.Diagnostics.Debug.WriteLine("API Contend: "+ reminder.getContend());
                

                resulter.Timer = reminder.getTimer(request);
                resulter.Contend = reminder.getContend();
                resulter.Title = "None";
                resulter.Type = "Reminder";

            }

            if (request_type.Equals("none"))
            {

                resulter.Contend = "không thể thực hiện yêu cầu";
                resulter.Type = "None";
                resulter.Title = "None";
                resulter.Timer = "None";

            }

    
            yield return resulter;
        }

     



        // POST api/values
        public void Post([FromBody]string value)
        {
        }


    }
}
