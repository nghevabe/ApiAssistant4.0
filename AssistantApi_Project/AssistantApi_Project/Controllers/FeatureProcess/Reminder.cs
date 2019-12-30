using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AssistantApi_Project.Controllers.FeatureProcess
{
    public class Reminder
    {//
        
        string request_type = "none";
        string string_temp = "";
      

        List<int> lstIndex = new List<int>();

        string[] lstSubject = { "tôi", "tao", "ta", "tớ", "mình" };

        string[] lstUnitWorld = { "cái", "chiếc" };

        string[] lstVerbOrder = { "hãy", "xin hãy", "làm ơn", "tìm hiểu", "muốn", "giúp", "cần" };

        string[] lstPreposition = { "của", "ở", "về", "cho", "vào", "lúc" };

        string[] lstObjectReminder = { "lịch", "hẹn" };

        string[] lstObjectDay = { "ngày", "hôm" };

        string[] lstVerbReminder = { "nhắc", "đặt", "xếp", "hẹn" };

        //
        string[] lstAdverb = { "nay", "mai", "hôm nay", "ngày mai", "ngày kia", "tuần này", "tuần sau" };



        public string Tester()
        {

            DateTime aDateTime = new DateTime(2012, 2, 4, 10, 15, 30);
            //TimeSpan addMoreTime = new System.TimeSpan(20, 9, 30, 0);
            //aDateTime = aDateTime.Date + addMoreTime;
            return aDateTime.ToString();

        } //

        public string getDayOfWeek(DateTime date)
        {

            string day_ofweek = date.DayOfWeek.ToString();
            return day_ofweek;

        } // 

        public string getCurrentTime()
        {
            DateTime aDateTime = DateTime.Now;
            string now_timer = DateTime.Now.ToString();

            return now_timer;
        }

        public string getAddTime(int day, int hour, int minute)
        {
            DateTime aDateTime = DateTime.Now;
            TimeSpan addMoreTime = new System.TimeSpan(day, hour, minute, 0);

            DateTime newTime = aDateTime.Add(addMoreTime);

            string string_newTime = newTime.ToString();

            return string_newTime;
        }


        public string getSubtractTime(int day, int hour, int minute)
        {
            DateTime aDateTime = DateTime.Now;
            TimeSpan addMoreTime = new System.TimeSpan(day, hour, minute, 0);

            DateTime newTime = aDateTime.Subtract(addMoreTime);

            string string_newTime = newTime.ToString();

            return string_newTime;
        }

        public string getTimer(string input) // 12/26/2019 5:41:32 PM
        {
            string_temp = input;
            // t quên nói với c 1 điều quan trọng
            string[] lstType1 = { "phút nữa", "giờ nữa", "tiếng nữa" };

            string result = "";
            //DateTime aDateTime = DateTime.Now;

            string[] input_cut = input.Split(' ');

            if (input.Contains("nữa"))
            {
                

                for (int i = 0; i < input_cut.Length; i++)
                {

                    if (input_cut[i].Equals("nữa"))
                    {
                        System.Diagnostics.Debug.WriteLine("text: " + input_cut[i - 1]);
                        System.Diagnostics.Debug.WriteLine("number: " + input_cut[i - 2]);

                        string value = input_cut[i - 2];
                        int int_value = Convert.ToInt32(value);

                        Remove_TimeWord(i);
                        Remove_TimeWord(i - 2);

                        if (input_cut[i - 1].Equals("phút"))
                        {

                            string newtime = getAddTime(0, 0, int_value);
                            System.Diagnostics.Debug.WriteLine("Final: " + newtime);

                            Remove_TimeWord(i - 1);

                        }


                        if (input_cut[i - 1].Equals("giờ") || input_cut[i - 1].Equals("tiếng"))
                        {

                            string newtime = getAddTime(0, int_value, 0);
                            System.Diagnostics.Debug.WriteLine("Final: " + newtime);

                            Remove_TimeWord(i - 1);

                        }
                    }



                  

                }
            }

            if (!input.Contains("nữa"))
            {

                string hour_destination = "none";
                string minute_destination = "none";
                int hour_need = 0;          
                DateTime aDateTime = DateTime.Now;

                TimeSpan timeSpan = new System.TimeSpan(0, 0, 0, 0);

                string second_now = aDateTime.Second.ToString();
                int int_second_now = Convert.ToInt32(second_now);

                string minute_now = aDateTime.Minute.ToString();
                int int_minute_now = Convert.ToInt32(minute_now);

                string hour_now = aDateTime.Hour.ToString();
                int int_hour_now = Convert.ToInt32(hour_now);

                timeSpan = new System.TimeSpan(0, 0, int_minute_now, int_second_now);
                aDateTime = aDateTime.Subtract(timeSpan);
                System.Diagnostics.Debug.WriteLine(" Timer0: " + aDateTime.ToString());

                // Get Hour





                for (int i = 0; i < input_cut.Length; i++)
                {
                    // Lấy Giờ
                    if (input_cut[i].Equals("giờ"))
                    {
                        // Trừ phút và giây về mốc 0
                        hour_destination = input_cut[i - 1];
                        minute_destination = "0";
                        Remove_TimeWord(i);
                        Remove_TimeWord(i - 1);

                        Regex regex = new Regex(@"^[0-9]+$");

                        if(i < (input_cut.Length - 1))
                        { 

                            if (regex.IsMatch(input_cut[i + 1])  )
                              {
                                   // System.Diagnostics.Debug.WriteLine("Is number");
                                   minute_destination = input_cut[i + 1];

                                  if(input_cut[i + 2].Equals("phút"))
                                    {
                                       Remove_TimeWord(i+2);
                                    }

                               }

                            if (input_cut[i + 1].Equals("kém") )
                            {
                                Remove_TimeWord(i + 1);

                                if (i < (input_cut.Length - 2))
                                {

                                    if (regex.IsMatch(input_cut[i + 2]) )
                                    {
                                        // System.Diagnostics.Debug.WriteLine("Is number");
                                        minute_destination = input_cut[i + 2];
                                        minute_destination = "-" + minute_destination;
                                        Remove_TimeWord(i + 2);

                                        if (i < (input_cut.Length - 3))
                                        {

                                            if (input_cut[i + 3].Equals("phút"))
                                            {
                                                Remove_TimeWord(i + 3);
                                            }

                                        }

                                    }
                                }

                            }

                        }



                    }

                    if (input_cut[i].Equals("rưỡi"))
                    {
                        // Trừ phút và giây về mốc 0
                        hour_destination = input_cut[i - 1];
                        minute_destination = "30";
                        Remove_TimeWord(i);
                        Remove_TimeWord(i - 1);

                    }


                }

                for (int i = 0; i < input_cut.Length; i++)
                {
                    if (input_cut[i].Equals("sáng") || input_cut[i].Equals("trưa"))
                    {
                        if (!hour_destination.Equals("none"))
                        {
                            DateTime TimeNow = DateTime.Now;

                            string hour_current = aDateTime.Hour.ToString();
                            int int_hour_current = Convert.ToInt32(hour_current);
                            int int_hour_destination = Convert.ToInt32(hour_destination);

                            int int_minute = Convert.ToInt32(minute_destination);

                            hour_need = int_hour_destination - int_hour_current;

                            timeSpan = new System.TimeSpan(0, hour_need, int_minute, 0);
                            aDateTime = aDateTime.Add(timeSpan);
                            Remove_TimeWord(i);
                            System.Diagnostics.Debug.WriteLine(" Timer1: " + aDateTime.ToString());

                        }


                    }


                }

                for (int i = 0; i < input_cut.Length; i++)
                {
                    if (input_cut[i].Equals("chiều") || input_cut[i].Equals("tối"))
                    {
                        if (!hour_destination.Equals("none"))
                        {
                            DateTime TimeNow = DateTime.Now;

                            string hour_current = aDateTime.Hour.ToString();
                            int int_hour_current = Convert.ToInt32(hour_current);
                            int int_hour_destination = Convert.ToInt32(hour_destination);

                            int int_minute = Convert.ToInt32(minute_destination);

                            hour_need = (int_hour_destination + 12) - int_hour_current;

                            timeSpan = new System.TimeSpan(0, hour_need, int_minute, 0);
                            aDateTime = aDateTime.Add(timeSpan);
                            Remove_TimeWord(i);
                            System.Diagnostics.Debug.WriteLine(" Timer: " + aDateTime.ToString());
                        }


                    }


                }

                /// xxx
                if (!input.Equals("sáng") || !input.Equals("trưa") || !input.Equals("chiều") || !input.Equals("tối"))
                {
                    if (!hour_destination.Equals("none"))
                    {
                        DateTime TimeNow = DateTime.Now;

                        string hour_current = aDateTime.Hour.ToString();
                        int int_hour_current = Convert.ToInt32(hour_current);
                        int int_hour_destination = Convert.ToInt32(hour_destination);

                        int int_minute = Convert.ToInt32(minute_destination);

                        hour_need = int_hour_destination - int_hour_current;

                        timeSpan = new System.TimeSpan(0, hour_need, int_minute, 0);
                        aDateTime = aDateTime.Add(timeSpan);
                      
                        System.Diagnostics.Debug.WriteLine(" Timer1: " + aDateTime.ToString());

                    }
                }


                    for (int i = 0; i < input_cut.Length; i++)
                {
                    if (input_cut[i].Equals("nay"))
                    {
                        timeSpan = new System.TimeSpan(0, 0, 0, 0);
                        aDateTime = aDateTime.Add(timeSpan);
                        Remove_TimeWord(i);


                    }

                    if (input_cut[i].Equals("mai"))
                    {
                        timeSpan = new System.TimeSpan(1, 0, 0, 0);
                        aDateTime = aDateTime.Add(timeSpan);
                        Remove_TimeWord(i);


                    }

                    if (input_cut[i].Equals("kia"))
                    {
                        timeSpan = new System.TimeSpan(2, 0, 0, 0);
                        aDateTime = aDateTime.Add(timeSpan);
                        Remove_TimeWord(i);


                    }



                }



                for (int i = 0; i < input_cut.Length; i++)
                {
                    if (input_cut[i].Equals("thứ"))
                    {
                        // Trừ phút và giây về mốc 0
                        DateTime TimeNow = DateTime.Now;

                        string dayTo = input_cut[i + 1];

                        string dayFrom = TimeNow.DayOfWeek.ToString();
                        dayFrom = getDayofWeek(dayFrom);

                        int int_day = Convert.ToInt32(dayTo) - Convert.ToInt32(dayFrom);

                        System.Diagnostics.Debug.WriteLine("Day: " + int_day);

                        timeSpan = new System.TimeSpan(int_day, 0, 0, 0);
                        aDateTime = aDateTime.Add(timeSpan);
                        Remove_TimeWord(i);
                        Remove_TimeWord(i+1);

                    }



                }


                for (int i = 0; i < input_cut.Length; i++)
                {
                    if (input_cut[i].Equals("tuần") && i < (input_cut.Length - 1))
                    {
                        Remove_TimeWord(i);

                        if (input_cut[i + 1].Equals("này") )
                        {
                            timeSpan = new System.TimeSpan(0, 0, 0, 0);
                            aDateTime = aDateTime.Add(timeSpan);
                            Remove_TimeWord(i+1);
                        }

                        if (input_cut[i + 1].Equals("tới") || input_cut[i + 1].Equals("sau"))
                        {
                            timeSpan = new System.TimeSpan(7, 0, 0, 0);
                            aDateTime = aDateTime.Add(timeSpan);
                            Remove_TimeWord(i + 1);
                        }

                    }


                }

                //System.Diagnostics.Debug.WriteLine("Final Timer: " + aDateTime.ToString());
                result = aDateTime.ToString();
                //System.Diagnostics.Debug.WriteLine("Final String: " + string_temp);

                //   SimplifyString(string_temp);
                //  Remove_AuxiliaryWords();


                // System.Diagnostics.Debug.WriteLine("Finally: " + SimplifyString_level2());


            }

                return result;
        }

        public string getContend()
        {

            SimplifyString(string_temp);
            Remove_AuxiliaryWords();

            //System.Diagnostics.Debug.WriteLine("Contend Result: " + GetContendResult());

            return GetContendResult();

        }

    // t là người có thể đem lại cho tất cả mọi người xung quanh sự an tâm và tin tưởng
    // nhưng đó chưa phải là vấn đề quan trọng, quan trọng nhất trong 1 mối quan hệ đó là cảm xúc
    // để có thể lừa đc c mãi mãi

        public string getDayofWeek(string day)
        {
            string result = "";

            if (day.Equals("Monday"))
            {
                result = "2";
            }

            if (day.Equals("Tuesday"))
            {
                result = "3";
            }

            if (day.Equals("Wenday"))
            {
                result = "4";
            }

            if (day.Equals("Thirday"))
            {
                result = "5";
            }

            if (day.Equals("Friday"))
            {
                result = "6";
            }

            if (day.Equals("Saturday"))
            {
                result = "7";
            }

            if (day.Equals("Sunday"))
            {
                result = "8";
            }

            return result;
        }


        public void Remove_TimeWord( int index_remove)
        {
            

            string result = "";

            string[] str_cut = string_temp.Split(' ');

            str_cut[index_remove] = "+";

            for(int i = 0; i < str_cut.Length; i++)
            {
                if (i == 0)
                {
                    result = str_cut[i];
                }
                if (i > 0)
                {
                    result = result + " " + str_cut[i];
                }
            }

            System.Diagnostics.Debug.WriteLine("Result: "+result);

            string_temp = result;

           
        }

       
       

        public void SimplifyString(string input)
        {

            for (int i = 0; i < lstSubject.Length; i++)
            {
                if (input.Contains(lstSubject[i]))
                {
                    RemoveWold(input, lstSubject[i]);
                }
            }

            for (int i = 0; i < lstVerbReminder.Length; i++)
            {
                if (input.Contains(lstVerbReminder[i]))
                {
                    RemoveWold(input, lstVerbReminder[i]);
                }
            }

            for (int i = 0; i < lstObjectReminder.Length; i++)
            {
                if (input.Contains(lstObjectReminder[i]))
                {
                    RemoveWold(input, lstObjectReminder[i]);
                }
            }

            for (int i = 0; i < lstVerbOrder.Length; i++)
            {
                if (input.Contains(lstVerbOrder[i]))
                {
                    RemoveWold(input, lstVerbOrder[i]);
                }
            }

       

            string[] temp_cut = string_temp.Split(' ');
          
      
            for (int i = 0; i < lstIndex.Count; i++)
            {

                for (int j = 0; j < temp_cut.Length; j++)
                {
                    if (lstIndex[i] == j)
                    {
                        temp_cut[j] = "*";
                    }
                }

            }

           

            //System.Diagnostics.Debug.WriteLine("Finally: " + str_final);
            Remove_AuxiliaryWords();


        }
        // AuxiliaryWords
        public void Remove_AuxiliaryWords()
        {
            string[] temp_cut = string_temp.Split(' ');
           

            for (int i = 0; i < lstIndex.Count; i++)
            {

                for (int j = 0; j < temp_cut.Length; j++)
                {
                    if (lstIndex[i] == j)
                    {
                        temp_cut[j] = "*";
                    }
                }

            }

            //
            for (int i = 0; i < temp_cut.Length; i++)
            {

                for (int j = 0; j < lstPreposition.Length; j++)
                {
                    if (temp_cut[i].Equals(lstPreposition[j]))
                    {
                       
                        if (temp_cut[i + 1].Equals("*"))
                        {
                            RemoveWold(string_temp, lstPreposition[j]);
                        }
                    }
                }

            }


            //
            for (int i = 0; i < temp_cut.Length; i++)
            {

                for (int j = 0; j < lstUnitWorld.Length; j++)
                {
                    if (temp_cut[i].Equals(lstUnitWorld[j]))
                    {
                       
                        if (temp_cut[i + 1].Equals("*"))
                        {
                            RemoveWold(string_temp, lstUnitWorld[j]);
                        }
                    }
                }

            }

            //
            for (int i = 0; i < temp_cut.Length; i++)
            {

                for (int j = 0; j < lstPreposition.Length; j++)
                {
                    if (temp_cut[i].Equals(lstPreposition[j]))
                    {

                        if (temp_cut[i + 1].Equals("+"))
                        {
                            RemoveWold(string_temp, lstPreposition[j]);

                            if(temp_cut[i - 1].Equals("vào"))
                            {
                                RemoveWold(string_temp, temp_cut[i - 1]);
                            }
                        }
                    }
                }

            }

            for (int i = 0; i < temp_cut.Length; i++)
            {

                for (int j = 0; j < lstObjectDay.Length; j++)
                {
                    if (temp_cut[i].Equals(lstObjectDay[j]))
                    {

                        if (temp_cut[i + 1].Equals("+"))
                        {
                            RemoveWold(string_temp, lstObjectDay[j]);
                        }
                    }
                }

            }

           

           

        }

        public string GetContendResult()
        {

            string[] temp_cut = string_temp.Split(' ');
            string str_final = "";

            for (int i = 0; i < lstIndex.Count; i++)
            {

                for (int j = 0; j < temp_cut.Length; j++)
                {
                    if (lstIndex[i] == j)
                    {
                        temp_cut[j] = "*";
                    }
                }

            }

            int count = 0;
            for (int i = 0; i < temp_cut.Length; i++)
            {
                if (!temp_cut[i].Equals("*") && !temp_cut[i].Equals("+"))
                {

                    if (count == 0)
                    {
                        str_final = str_final + temp_cut[i];
                    }

                    if (count > 0)
                    {
                        str_final = str_final + " " + temp_cut[i];
                    }
                    count++;
                }
            }

            return str_final;
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