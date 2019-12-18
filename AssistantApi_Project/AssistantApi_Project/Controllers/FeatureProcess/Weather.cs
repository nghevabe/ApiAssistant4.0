using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Nancy.Json;
using System.Net;
using System.Text;
using AssistantApi_Project.Models;
using AssistantApi_Project.Controllers.CommonProcess;


namespace AssistantApi_Project.Controllers.FeatureProcess
{

    //http://api.openweathermap.org/data/2.5/forecast?q=Hanoi&appid=453834abf4e4465bbd71a8221489040e

    public class Weather
    {

       // public static List<WeatherObject> lstWeathers = new List<WeatherObject>();


        public List<WeatherObject> GetDataWeather3days()
        {
            List<WeatherObject> lstWeathers = new List<WeatherObject>();

            HttpData httpData = new HttpData();
            string json = httpData.StringFromUrl("http://api.openweathermap.org/data/2.5/forecast?q=Hanoi&appid=453834abf4e4465bbd71a8221489040e");

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(json);

            object timeToday = dobj["list"][0]["dt_txt"].ToString();
            string currentDay = "Hôm nay";
            
            string currentTimeToday = timeToday.ToString();
            string[] strTimeTodayCut = currentTimeToday.Split(' ');
            string strToday = strTimeTodayCut[0];
            string strCurrentDay = strToday;

            for (int i = 0; i < 20; i++)
            {

                object temp = dobj["list"][i]["main"]["temp"].ToString();
                object status = dobj["list"][i]["weather"][0]["description"].ToString();
                object time = dobj["list"][i]["dt_txt"].ToString();

                WeatherObject weatherObject = new WeatherObject();

                weatherObject.Temp = ConvertTempKtoTempC(temp.ToString());

                

                weatherObject.Status = status.ToString();
                weatherObject.Time = time.ToString();

                string[] strTimeCut = time.ToString().Split(' ');
                string strDay = strTimeCut[0];

                if (!strCurrentDay.Equals(strDay.ToString()) && currentDay.Equals("Hôm nay"))
                {
                    currentDay = "Ngày mai";
                    strCurrentDay = strDay.ToString();
                }

                if (!strCurrentDay.Equals(strDay.ToString()) && currentDay.Equals("Ngày mai"))
                {
                    currentDay = "Ngày kia";
                    strCurrentDay = strDay.ToString();
                }

                weatherObject.Day = currentDay;

                lstWeathers.Add(weatherObject);

            }


            WeatherObject datageted = new WeatherObject();
            datageted = lstWeathers[0];
         
            return lstWeathers;

        }

        /*
        public string TestDay()
        {
            List<WeatherObject> lstWeather = GetDataWeather3days();
            WeatherObject datageted = new WeatherObject();
            datageted = lstWeather[0];
            string chuoiTest = lstWeather[0].Day.ToString() + lstWeather[0].Temp.ToString()
                + " - " + lstWeather[1].Day.ToString() + lstWeather[1].Temp.ToString()
                + " - " + lstWeather[2].Day.ToString() + lstWeather[2].Temp.ToString()
                + " - " + lstWeather[3].Day.ToString() + lstWeather[3].Temp.ToString()
                + " - " + lstWeather[4].Day.ToString() + lstWeather[4].Temp.ToString()
                + " - " + lstWeather[5].Day.ToString() + lstWeather[5].Temp.ToString()
                + " - " + lstWeather[6].Day.ToString() + lstWeather[6].Temp.ToString()
                + " - " + lstWeather[7].Day.ToString() + lstWeather[7].Temp.ToString()
                + " - " + lstWeather[8].Day.ToString() + lstWeather[8].Temp.ToString()
                + " - " + lstWeather[9].Day.ToString() + lstWeather[9].Temp.ToString()
                + " - " + lstWeather[10].Day.ToString() + lstWeather[10].Temp.ToString()
                + " - " + lstWeather[11].Day.ToString() + lstWeather[11].Temp.ToString()
                + " - " + lstWeather[12].Day.ToString() + lstWeather[12].Temp.ToString()
                + " - " + lstWeather[13].Day.ToString() + lstWeather[13].Temp.ToString()
                + " - " + lstWeather[14].Day.ToString() + lstWeather[14].Temp.ToString()
                + " - " + lstWeather[15].Day.ToString() + lstWeather[15].Temp.ToString();

            return chuoiTest;
        }
        */

        public string GetTemperature(string dayRequest)
        {
            int temp_consume = 0;
            int temp_tb = 0;
            int counter = 0;

            List<WeatherObject> lstWeather = GetDataWeather3days();
            WeatherObject datageted = new WeatherObject();

            for(int i=0; i < lstWeather.Count; i++)
            {
                datageted = lstWeather[i];
                if(datageted.Day.Equals(dayRequest))
                {
                    int temp = (int) System.Convert.ToInt32(datageted.Temp);
                    temp_consume = temp_consume + temp;
                    
                    counter++;
                    

                }

            }

            temp_tb = (int) (temp_consume / counter);
           // Console.WriteLine("temp: " + counter);

            // datageted = lstWeather[0];


            return "Nhiệt độ "+dayRequest+" là " + temp_tb.ToString() + " độ C";
        }

        public string GetStatusRain(string dayRequest)
        {
            string response = dayRequest + " trời không mưa";

            List<WeatherObject> lstWeather = GetDataWeather3days();
            //WeatherObject datageted = new WeatherObject();

            for(int i = 0; i < lstWeather.Count; i++)
            {
                WeatherObject datageted = new WeatherObject();
                datageted = lstWeather[i];

                if (dayRequest.Equals(datageted.Day))
                {
                    if (datageted.Status.Contains("rain"))
                    {

                        response = dayRequest + " trời có mưa vào lúc " + GetHour(datageted.Time.ToString()) ;
                        break;

                    }
                }
            }
               

            //datageted = lstWeather[0];


            return  response;
        }

        public string GetHour(string string_time)
        {
            string[] string_cut = string_time.Split(' ');

            string[] time_cut = string_cut[1].Split(':');

            string hour = time_cut[0].ToString();

            int int_hour = Convert.ToInt32(hour);

            if(int_hour < 12)
            {
                hour = hour + " giờ sáng";
            }

            if (int_hour > 12)
            {
                int_hour = int_hour - 12;
                hour = int_hour.ToString();
                hour = hour + " giờ tối";
            }
            //
            return hour;
        }

        public string GetAllInfo(string dayRequest)
        {
            string response = "";

            int temp_consume = 0;
            int temp_tb = 0;
            int counter = 0;

            int rain = 0;

            List<WeatherObject> lstWeather = GetDataWeather3days();
            WeatherObject datageted = new WeatherObject();

            for (int i = 0; i < lstWeather.Count; i++)
            {
                datageted = lstWeather[i];
                if (datageted.Day.Equals(dayRequest))
                {
                    int temp = (int)System.Convert.ToInt32(datageted.Temp);
                    temp_consume = temp_consume + temp;

                    counter++;

                    if (datageted.Status.Contains("rain"))
                    {

                        
                        rain = 1;


                    }


                }

            }

            temp_tb = (int)(temp_consume / counter);

            if (rain == 0)
            {

                response = dayRequest + " trời không mưa và nhiệt độ là " + temp_tb ;

            }

            if (rain == 1)
            {

                response = dayRequest + " trời có mưa vào lúc "+ GetHour(datageted.Time.ToString()) + " và nhiệt độ là " + temp_tb;

            }


            return response;
        }


        public string ConvertTempKtoTempC(string temp)
        {
            string result = "";

            float tempK = float.Parse(temp);
            float tempC = tempK - 273;
            tempC = (int)tempC;

            result = tempC.ToString();


            return result;
        }


        public string getWeatherResponse(string request)
        {
            string response = "XXX";
            string day = getDayRequest(request);
            if (request.Contains("nhiệt độ") || request.Contains("độ"))
            {
                
                response = GetTemperature(day);
            }

            if (request.Contains("mưa"))
            {
                response = GetStatusRain(day);
            }

            if (request.Contains("thời tiết"))
            {
                response = GetAllInfo(day);
            }

            return response;
        }

        public string getDayRequest(string request)
        {
            string result = "";

            if(request.Contains("hôm nay") || request.Contains("nay"))
            {
                result = "Hôm nay";
            }

            if (request.Contains("ngày mai") || request.Contains("mai"))
            {
                result = "Ngày mai";
            }

            if (request.Contains("ngày kia"))
            {
                result = "Ngày kia";
            }

            return result;
        }
     

    }
}