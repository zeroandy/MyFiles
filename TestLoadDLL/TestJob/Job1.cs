using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestJob
{

    public class Job1
    {
        public string GetName(string input) {
            List<string> output = new List<string> { input,"2" };
            return JsonConvert.SerializeObject(output);
        }
    }
}
