using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_SOM
{
    public class Point
    {
        int x;
        int y;
        public int X
        {
            get
            {
                return x;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
        }

        public void SetPoint(int i, int j)
        {
            x = i;
            y = j;
        }

        public string Print()
        {
            return "(x,y) = (" + x + "," + y + ")";
        }
    }
}
