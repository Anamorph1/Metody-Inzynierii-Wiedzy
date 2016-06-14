using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Globalization;

namespace miw_KNN.Resources
{
    public static class Converters
    {
        public static double Convert(string input)
        {
            double value;
            Double.TryParse(input, out value);
            return value;
        }
    }
}
