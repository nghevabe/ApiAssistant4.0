using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Controllers.FeatureProcess
{
    public class DeviceController
    {
        string string_temp = "";
        List<int> lstIndex = new List<int>();

        string[] lstSubject = { "tôi", "tao", "ta", "tớ", "mình" };

        string[] lstVerbOrder = { "hãy", "xin hãy", "làm ơn", "tìm hiểu" };

        string[] lstVerbController = { "bật", "mở", "tắt", "đóng", "tăng", "giảm", "chuyển", "đổi", "thay đổi" };

        string[] lstObjectController = { "đèn", "thiết bị", "cửa", "quạt", "độ sáng", "ánh sáng", "mức sáng" };

        string[] lstPreposition = { "của", "ở", "về", "cho", "vào", "lúc" };

        string[] lstColor = { "đỏ", "xanh lá cây","xanh lam","xanh da trời","xanh nước biến","xanh", "vàng", "tím", "trắng" };

        string[] lstTypeColor = { "RED", "GREEN", "AQUA", "AQUA", "BLUE", "BLUE", "YELLOW", "VIOLET", "WHITE" };


        public string getCommandType(string input)
        {
            string result = "none";

            if (input.Contains("bật") || input.Contains("mở"))
            {

                result = "ON";

            }

            if (input.Contains("tắt") || input.Contains("đóng"))
            {

                result = "OFF";

            }

            if (input.Contains("tăng") )
            {

                result = "UP";

            }

            if (input.Contains("giảm"))
            {

                result = "DOWN";

            }

            for(int i = 0; i < lstColor.Length; i++)
            {

                if (input.Contains(lstColor[i])){
                    result = lstTypeColor[i];
                }

            }

            return result;

        }


        public string getDeviceName(string input)
        {

            string result = SimplifyString(input);

            return result;

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

        public string SimplifyString(string strRequest)
        {

       

            string_temp = strRequest;



            for (int i = 0; i < lstSubject.Length; i++)
            {
                if (strRequest.Contains(lstSubject[i]))
                {
                    RemoveWold(strRequest, lstSubject[i]);
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

            for (int i = 0; i < lstVerbController.Length; i++)
            {
                if (strRequest.Contains(lstVerbController[i]))
                {
                    RemoveWold(strRequest, lstVerbController[i]);
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


            //System.Diagnostics.Debug.WriteLine("Final String: " + str_final);

            return str_final;
        }

    }
}