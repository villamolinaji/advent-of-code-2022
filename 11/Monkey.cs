using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11
{
    public class Monkey
    {
        public int Index { get; set; }
        public List<long> Items { get; set; }

        public bool IsSum { get; set; }
        public long OperationValue { get; set; }
        public long DivisibleValue { get; set; }
        public int IfTrueMonkey { get; set; }
        public int IfFalseMonkey { get; set; }
        public long InspectedCount { get; set; }

    }
}
