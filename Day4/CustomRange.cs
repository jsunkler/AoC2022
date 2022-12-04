using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class CustomRange
    {

        public int LowerIndex { get; set; }
        public int UpperIndex { get; set; }

        public bool Includes(CustomRange otherRange)
        {
            if (otherRange.LowerIndex >= LowerIndex && otherRange.UpperIndex <= UpperIndex) return true;
            return false;
        }

        public CustomRange()
        {

        }

        public CustomRange(int lowerIndex, int upperIndex)
        {
            LowerIndex = lowerIndex;
            UpperIndex = upperIndex;
        }
    }
}
