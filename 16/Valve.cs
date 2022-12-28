using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16
{
    public class Valve
    {
        public string Id { get; set; }
        public int Rate { get; set; }
        public List<string> Destinations { get; set; }
    }
}
