using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssistantApi_Project.Controllers.CommonProccess;
using AssistantApi_Project.Controllers.FeatureProcess;


namespace AssistantApi_Project.Controllers
{
    public class ValuesController : ApiController
    {
      
        public IEnumerable<string> GetResponse(string request)
        {
          
         

            return new string[] { "NONE" };
        }

   







        // POST api/values
        public void Post([FromBody]string value)
        {
        }

     
    }
}
