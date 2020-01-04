using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Models
{
    public class Resulter
    {

        private string contend;
        private string timer;
        private string type;
        private string title;
        //private string finalResponse;

        public string Contend
        {
            get { return contend; }
            set { contend = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /*
        public string FinalResponse
        {
            get { return finalResponse; }
            set { finalResponse = value; }
        }
        */

    }
}