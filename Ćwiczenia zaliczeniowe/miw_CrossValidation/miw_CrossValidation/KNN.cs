using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_CrossValidation
{
    public static class SimpleKNN
    {
        //Metoda KNN//
        public static T KNN<T>(IEnumerable<T> input, int k) where T : IComparable 
        {
            var outputItem = input.OrderBy(x => x).
                Take(k).GroupBy(x => x).
                OrderByDescending(x => x.Count()).
                Select(grp => grp.Key).
                First();
            return outputItem;
        }
    }
}
