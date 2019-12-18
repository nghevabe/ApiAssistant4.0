using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Models
{
    public class Resulter
    {

        private string contend;
        private string type;
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

        /*
        public string FinalResponse
        {
            get { return finalResponse; }
            set { finalResponse = value; }
        }
        */

    }
}