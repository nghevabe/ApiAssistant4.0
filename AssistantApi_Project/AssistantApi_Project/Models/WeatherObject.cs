using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Models
{
    public class WeatherObject
    {


        private string temp;
        private string status;
        private string time;
        private string day;


        public string Temp
        {
            get { return temp; }
            set { temp = value; }
        }


        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Day
        {
            get { return day; }
            set { day = value; }
        }
    }
}