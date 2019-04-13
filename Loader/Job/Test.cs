using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    public class Dog{
        public string Name { get; set; }
     }
    public class Test
    {
        public string Get() {
            return JsonConvert.SerializeObject(new Dog { Name = "Bob"});
        }
    }
}
