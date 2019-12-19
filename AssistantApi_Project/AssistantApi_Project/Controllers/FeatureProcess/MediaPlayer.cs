using AssistantApi_Project.Controllers.CommonProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Controllers.FeatureProcess
{
    public class MediaPlayer
    {


        public void getSourceOnSearch()
        {


            HttpData httpData = new HttpData();
            string key = "anh là ai";
            string str = httpData.StringFromUrl("https://nhacpro.net/tim-kiem.html?q="+key);

            string linkHtml = getSourceOnLink(str);


            getLinkMp3(getSourceOnLinkHtml(linkHtml));





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

        public void getLinkMp3(string source)
        {

            string signMp3 = "<source src=\"";
            int item_mp3 = source.IndexOf(signMp3, 0);

            int end_mp3 = source.IndexOf(".mp3", item_mp3 + (signMp3.Length));

            string linkMp3 = source.Substring(item_mp3 + (signMp3.Length), (end_mp3 - (item_mp3 + (signMp3.Length))));

            linkMp3 = linkMp3 + ".mp3";

            System.Diagnostics.Debug.WriteLine("|" + linkMp3 + "|");

            //return linkMp3;
        }

    }
}