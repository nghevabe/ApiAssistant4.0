using AssistantApi_Project.Controllers.CommonProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Controllers.FeatureProcess
{
    public class MediaPlayer
    {

        string request_type = "none";
        string string_temp = "";
        List<int> lstIndex = new List<int>();

        string[] lstSubject = { "tôi", "tao", "ta", "tớ", "mình" };

        string[] lstVerbOrder = { "hãy", "xin hãy", "làm ơn", "tìm hiểu" };

        string[] lstVerbMedia = { "bật", "mở", "chơi", "chạy", "nghe" };

        string[] lstObjectMedia = { "bài", "bài hát", "ca khúc", "nhạc" };


        public string getLinkMedia(string key)
        {

            HttpData httpData = new HttpData();
            //string key = "anh là ai";
            string str = httpData.StringFromUrl("https://nhacpro.net/tim-kiem.html?q="+key);

            string linkHtml = getSourceOnLink(str);

            return getLinkMp3(getSourceOnLinkHtml(linkHtml));

            

        }

        public string getSourceOnLink(string source)
        {

            //<source src
            //<div id="music"><h3><a href="
            string signSrc = "<div id=\"music\"><h3><a href=\"";
            int item_src = source.IndexOf(signSrc, 0);

            int end_item = source.IndexOf(".html", item_src + (signSrc.Length));

            string linkHTML = source.Substring(item_src + (signSrc.Length), (end_item - (item_src + (signSrc.Length))));

            linkHTML = linkHTML + ".html";

            System.Diagnostics.Debug.WriteLine("|" + linkHTML + "|");

            return linkHTML;

        }

        public string getSourceOnLinkHtml(string linkHtml)
        {
            HttpData httpData = new HttpData();
           
            string source = httpData.StringFromUrl(linkHtml);

            return source;
        }

        public string getLinkMp3(string source)
        {

            string signMp3 = "<source src=\"";
            int item_mp3 = source.IndexOf(signMp3, 0);

            int end_mp3 = source.IndexOf(".mp3", item_mp3 + (signMp3.Length));

            string linkMp3 = source.Substring(item_mp3 + (signMp3.Length), (end_mp3 - (item_mp3 + (signMp3.Length))));

            linkMp3 = linkMp3 + ".mp3";

            System.Diagnostics.Debug.WriteLine("|" + linkMp3 + "|");


            return linkMp3;
        }


        public string SimplifyStringForMedia(string strRequest)
        {
        

            string_temp = strRequest;
                 

            for (int i = 0; i < lstObjectMedia.Length; i++)
            {
                if (strRequest.Contains(lstObjectMedia[i]))
                {
                    string_temp = RemoveString(strRequest, lstObjectMedia[i]);
                }
            }
   

            return string_temp;
        }

        public string RemoveString(string str, string world)
        {

            int index1 = str.IndexOf(world);

            string str1 = str.Substring(0, index1);
            string str2 = str.Substring(index1 + world.Length, str.Length - (index1 + world.Length));


            // System.Diagnostics.Debug.WriteLine("chuoi_cut: "+str2);



            return str2;


        }


        public void RemoveWold(string str, string world)
        {

            int index1 = str.IndexOf(world);

            string str1 = str.Substring(0, index1);
            string str2 = str.Substring(index1 + world.Length, str.Length - (index1 + world.Length));

            string[] world_cut = world.Split(' ');
            int counter = world_cut.Length;

            string str_decoder = "";

            for (int i = 0; i < counter; i++)
            {
                if (i == 0)
                {
                    str_decoder = "*";
                }
                else
                {
                    str_decoder = str_decoder + " " + "*";
                }

            }

            string_temp = str1 + str_decoder + str2;

            string[] temp_cut = string_temp.Split(' ');

            for (int i = 0; i < temp_cut.Length; i++)
            {
                if (temp_cut[i].Equals("*"))
                {
                    lstIndex.Add(i);
                }
            }



        }

      

    }
}