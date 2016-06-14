using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace agds
{
    public static class TextMethod
    {
        public static string[] LineToTable(string inputFile, char space, int index)
        {
            try
            {
                string Line = File.ReadAllLines(inputFile).ElementAt(index);
                return Line.Split(space);
            }
            catch
            {
                return null;
            }

        }

        public static string[] LineToTable(string Line, char space)
        {
            return Line.Split(space);
        }

        public static double? ToDouble(this string s)
        {
            double result;
            if(double.TryParse(s, out result))
                return result;
            return null;
        }
    }
}
