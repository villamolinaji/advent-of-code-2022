using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16
{
    public class QueueItem
    {
        public string CurrentValve { get; set; }
        public int Minute { get; set; }
        public int Score { get; set; }
        public List<string> Opened { get; set; }

        public string SecondValve { get; set; }
    }
}
