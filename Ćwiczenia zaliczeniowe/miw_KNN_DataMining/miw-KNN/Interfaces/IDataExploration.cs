using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_KNN
{
    interface IDataExploration<T>
    {
        List<T> ReferenceListOfItems { get; }
        IEnumerable<bool> ElementContain(T key);
        double Support(T key);
        double ConditionSupportOr(T firstKey, T secondKey);
        double ConditionSupportAnd(T firstKey, T secondKey);
        List<T> Frequent(double refPercentage);
        double ConditionConfidence(T primaryKey, T secondaryKey);
        IEnumerable<List<int>> EquivalenceClassTransformation();
    }
}
