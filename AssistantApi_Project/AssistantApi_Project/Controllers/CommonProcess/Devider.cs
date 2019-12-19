using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Controllers.CommonProcess
{

    

    public class Devider
    {
        string request_type = "none";    
        List<int> lstIndex = new List<int>();
        // 

        string[] lstRequestType = { "wiki", "weather", "controller","media", "reminder" };
        int[] lstPoint = {0,0,0,0,0 };

        string[] lstAdverb = {"hôm nay", "ngày mai", "ngày kia","hiện giờ"};

       

        string[] lstVerbAsk = {"hỏi","biết","xem","tìm hiểu"};
        string[] lstVerbOrder = { "hãy", "xin hãy", "làm ơn", "tìm hiểu" };
        
        string[] lstWiki_Ask_What = { "là ai", "ai là", "là gì", "cái gì", "là cái gì" };

        string[] lstWiki_Ask_Where = { "ở đâu", "nằm ở đâu", "nằm đâu", "chỗ nào", "nằm ở chỗ nào", "nằm chỗ nào" };

        string[] lstWiki_Ask_How = { "thế nào","như nào","như thế nào","là sao","làm sao" };

      
        string[] lstWiki_Key_Before = { "chi tiết","thông tin", "tin tức" };

        string[] lstWeather_Key = { "thời tiết","nhiệt độ","có mưa không","trời mưa không"};

        string[] lstVerbMedia = { "bật", "mở", "chơi", "chạy", "nghe" };

        string[] lstObjectMedia = { "bài", "bài hát", "ca khúc", "nhạc" };


        public string RequestDevider(string strRequest)
        {
            CheckWiki(strRequest);
            CheckWeather(strRequest);
            CheckMedia(strRequest);

            for(int i=0; i< lstPoint.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine("Point: " + lstPoint[i]);
            }

            //System.Diagnostics.Debug.WriteLine("Type Max: " + lstRequestType[FindMaxIndex()]);
            if(FindMaxIndex() < 0)
            {
                request_type = "none";
            }

            if (FindMaxIndex() >= 0)
            {
                request_type = lstRequestType[FindMaxIndex()];
            }
            //request_type = lstRequestType[FindMaxIndex()];

            return request_type;
        }
        // tôi muốn biết thông tin về thành cổ sơn tây

        public int FindMaxIndex()
        {

            int max = 0;
            int index = -1;

            for(int i = 0; i < lstPoint.Length; i++)
            {
                if(lstPoint[i] > max)
                {
                    max = lstPoint[i];
                    index = i;
                }
            }

            return index;
        }

        public void CheckWiki(string strRequest)
        {
            int point = 0;

            for (int i = 0; i < lstVerbAsk.Length; i++)
            {
                if (strRequest.Contains(lstVerbAsk[i]))
                {

                    point = point + 1;


                }
            }

            int pointWhat = 0;
            for (int i = 0; i < lstWiki_Ask_What.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Ask_What[i]))
                {
                    request_type = "wiki";
                    pointWhat = 1;
                }
            }

            for (int i = 0; i < lstWiki_Ask_Where.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Ask_Where[i]))
                {
                    request_type = "wiki";
                    pointWhat = 1; ;
                }
            }

            for (int i = 0; i < lstWiki_Ask_How.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Ask_How[i]))
                {
                    request_type = "wiki";
                    pointWhat = 1;
                }
            }

            point = point + pointWhat;

            for (int i = 0; i < lstWiki_Key_Before.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Key_Before[i]))
                {
                    request_type = "wiki";
                    point = point + 1;
                }
            }

            lstPoint[0] = point;


        }


        public void CheckWeather(string strRequest)
        {
            int point = 0;
            int status = 0;

            for (int i = 0; i < lstVerbAsk.Length; i++)
            {
                if (strRequest.Contains(lstVerbAsk[i]))
                {
                    
                    point = point + 1;


                }
            }

            for (int i = 0; i < lstWeather_Key.Length; i++)
            {
                if (strRequest.Contains(lstWeather_Key[i]))
                {
                    status = 1;
                    point = point + 1;


                }
            }

            if(status == 1)
            {
                for (int i = 0; i < lstAdverb.Length; i++)
                {
                    if (strRequest.Contains(lstAdverb[i]))
                    {                    
                        request_type = "weather";
                        point = point + 2;
                        
                    }
                }
            }

            lstPoint[1] = point;

           
        }


        public void CheckMedia(string strRequest)
        {
            int point = 0;

            for (int i = 0; i < lstVerbAsk.Length; i++)
            {
                if (strRequest.Contains(lstVerbAsk[i]))
                {

                    point = point + 1;


                }
            }


            //lstVerbMedia
            //lstObjectMedia


            for (int i = 0; i < lstVerbMedia.Length; i++)
            {
                if (strRequest.Contains(lstVerbMedia[i]))
                {

                    point = point + 1;


                }
            }

            for (int i = 0; i < lstObjectMedia.Length; i++)
            {
                if (strRequest.Contains(lstObjectMedia[i]))
                {

                    point = point + 1;


                }
            }





            lstPoint[3] = point;


        }






    }


  
}