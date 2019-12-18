using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantApi_Project.Controllers.CommonProccess
{
    public class ProcessString
    {

        //https://vi.m.wikipedia.org/w/api.php?action=opensearch&search=chi_pu&limit=1&format=json


        public string ReplaceSpace(string input)
        {

            string result = "";

            string[] input_cut = input.Split(' ');

            for(int i = 0; i < input_cut.Length; i++)
            {

                result = result + "_" + input_cut[i];

            }

            return result;


        }

        public string lowCaseString(string input)
        {


            return "";
        }

    }
}