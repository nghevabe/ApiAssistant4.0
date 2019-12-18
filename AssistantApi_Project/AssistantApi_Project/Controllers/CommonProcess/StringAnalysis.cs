using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Controllers.CommonProcess
{
    public class StringAnalysis
    {


        string request_type = "none";
        string string_temp = "";
        List<int> lstIndex = new List<int>();
        // 

        string[] lstRequestType = { "wiki", "weather", "controller", "musical", "reminder" };
        int[] lstPoint = { 0, 0, 0, 0, 0 };

        string[] lstAdverb = { "hôm nay", "ngày mai", "ngày kia", "hiện giờ" };

        string[] lstSubject = { "tôi", "tao", "ta", "tớ", "mình" };

        string[] lstVerbAsk = { "hỏi", "biết", "xem", "tìm hiểu" };

        string[] lstVerbOrder = { "hãy", "xin hãy", "làm ơn", "tìm hiểu" };

        string[] lstPreposition = { "của", "ở", "về", "cho" };

        string[] lstWiki_Ask_What = { "là ai", "ai là", "là gì", "cái gì", "là cái gì" };

        string[] lstWiki_Ask_Where = { "ở đâu", "nằm ở đâu", "nằm đâu", "chỗ nào", "nằm ở chỗ nào", "nằm chỗ nào" };

        string[] lstWiki_Ask_How = { "thế nào", "như nào", "như thế nào", "là sao", "làm sao" };

        string[] lstWiki_Key_Infomation = { "chi tiết", "thông tin", "tin tức" };

        string[] lstWeather_Key = { "thời tiết", "nhiệt độ", "có mưa không", "trời mưa không" };
        




        public string SimplifyString(string strRequest)
        {

            /*
            for (int i = 0; i < lstWiki_Key_Before.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Key_Before[i]))
                {
                    strRequest = RemoveStringBefore(strRequest, lstWiki_Key_Before[i]);
                }
            }
            */

            string_temp = strRequest;

            for (int i = 0; i < lstWiki_Key_Infomation.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Key_Infomation[i]))
                {
                    RemoveWold(strRequest, lstWiki_Key_Infomation[i]);
                }
            }


            for (int i = 0; i < lstSubject.Length; i++)
            {
                if (strRequest.Contains(lstSubject[i]))
                {
                    RemoveWold(strRequest, lstSubject[i]);
                }
            }


            for (int i = 0; i < lstVerbAsk.Length; i++)
            {
                if (strRequest.Contains(lstVerbAsk[i]))
                {
                    RemoveWold(strRequest, lstVerbAsk[i]);
                }
            }

            for (int i = 0; i < lstVerbOrder.Length; i++)
            {
                if (strRequest.Contains(lstVerbOrder[i]))
                {
                    RemoveWold(strRequest, lstVerbOrder[i]);
                }
            }



            for (int i = 0; i < lstPreposition.Length; i++)
            {
                if (strRequest.Contains(lstPreposition[i]))
                {
                    RemoveWold(strRequest, lstPreposition[i]);
                }
            }

            
            for (int i = 0; i < lstWiki_Ask_What.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Ask_What[i]))
                {
                    RemoveWold(strRequest, lstWiki_Ask_What[i]);
                }
            }


            for (int i = 0; i < lstWiki_Ask_Where.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Ask_Where[i]))
                {
                    RemoveWold(strRequest, lstWiki_Ask_Where[i]);
                }
            }

            for (int i = 0; i < lstWiki_Ask_How.Length; i++)
            {
                if (strRequest.Contains(lstWiki_Ask_How[i]))
                {
                    RemoveWold(strRequest, lstWiki_Ask_How[i]);
                }
            }




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
                if (!temp_cut[i].Equals("*"))
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


            System.Diagnostics.Debug.WriteLine("Final String: " + str_final);

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



        public List<string> SeparateString(string str)
        {
            string chuoi = str;
            string[] chuoi_array = chuoi.Split(' ');
            List<string> lstStr = new List<string>();

            for (int i = 2; i < chuoi_array.Length; i++)
            {


                //muốn văn mai hương


                for (int k = 0; k <= (chuoi_array.Length - i); k++)
                {
                    string chuoi_new = "";


                    for (int j = 0; j < i; j++)
                    {
                        if (j == 0)
                        {
                            chuoi_new = chuoi_new + chuoi_array[j + k];
                        }

                        if (j > 0)
                        {
                            chuoi_new = chuoi_new + " " + chuoi_array[j + k];
                        }
                    }

                    //System.Diagnostics.Debug.WriteLine("Chuoi: " + chuoi_new);
                    lstStr.Add(chuoi_new);

                }



            }

            return lstStr;
        }






    }
}