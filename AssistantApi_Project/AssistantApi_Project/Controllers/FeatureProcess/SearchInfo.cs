using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using AssistantApi_Project.Controllers.CommonProcess;

namespace AssistantApi_Project.Controllers.FeatureProcess
{
    public class SearchInfo
    {



      
        public string GetWikiInfomations(string input)
        {

            HttpData httpData = new HttpData();
            string str = httpData.StringFromUrl(input);
            string result = "";

            string signItem = "<Text xml:space=\"preserve\">";
            int item_index = str.IndexOf(signItem, 0);

            if(item_index <= 0)
            {
                result = "không có kết quả";
            }

            if (item_index > 0)
            {

                int end_item = str.IndexOf("</Text>", item_index + (signItem.Length));

                string objectTitle = str.Substring(item_index + (signItem.Length), (end_item - (item_index + (signItem.Length))));
                System.Diagnostics.Debug.WriteLine("Title: "+ objectTitle);

                objectTitle = objectTitle.ToLower();

                if (!ResponseController.keyworld_wiki.Contains(objectTitle))
                {
                    result = "không có kết quả";
                }

                if (ResponseController.keyworld_wiki.Contains(objectTitle))
                {

                    // anal
                    string signContend = "<Description xml:space=\"preserve\">";

                    int start_index = str.IndexOf(signContend, 0);

                    int end_index = str.IndexOf("</Description>", start_index + (signContend.Length));


                    result = str.Substring(start_index + (signContend.Length), (end_index - (start_index + (signContend.Length))));
                }
            }


            return result;
                  
        }








    }
}