using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using miw_KNN.Resources;

namespace miw_KNN
{
    public class TransactionSet<T> : IDataExploration<T>
    {
        public List<List<T>> dataSet { get; private set; }
        private readonly List<T> referenceListOfItems;
        public List<T> ReferenceListOfItems
        {
            get { return referenceListOfItems; }
        }

        public TransactionSet(List<T> ReferenceListOfItems)
        {
            dataSet = new List<List<T>>();
            referenceListOfItems = ReferenceListOfItems;
        }

        public void Add(List<T> Data)
        {
            dataSet.Add(Data.ToList());
        }

        public IEnumerable<bool> ElementContain(T key)
        {
            return dataSet.Select(x => x.Contains(key));
        }

        public double Support(T key)
        {
            var containList = this.ElementContain(key);
            double count = containList.LongCount(x => x == true);
            return count / (double)containList.Count();
        }

        public double ConditionSupportOr(T primaryKey, T secondaryKey)
        {
            var containFirst = this.ElementContain(primaryKey);
            var containSecond = this.ElementContain(secondaryKey);
            var containList = containFirst.Zip(containSecond, (x, y) => x || y);

            double count = containList.LongCount(x => x == true);
            return count / (double)containList.Count();
        }

        public double ConditionSupportAnd(T primaryKey, T secondaryKey)
        {
            var containFirst = this.ElementContain(primaryKey);
            var containSecond = this.ElementContain(secondaryKey);
            var containList = containFirst.Zip(containSecond, (x, y) => x && y);

            double count = containList.LongCount(x => x == true);
            return count / (double)containList.Count();
        }

        public List<T> Frequent(double refPercent)
        {
            List<T> outputList = new List<T>();
            foreach (var item in referenceListOfItems)
            {
                if (Support(item) > refPercent)
                    outputList.Add(item);
            }
            return outputList;
        }

        public double ConditionConfidence(T primaryKey, T secondaryKey)
        {
            var primaryContain = this.ElementContain(primaryKey);
            var secondaryContain = this.ElementContain(secondaryKey);
            var containList = primaryContain.Zip(secondaryContain, (x, y) => x && y);

            double primaryCount = primaryContain.LongCount(x => x == true);
            double conditionCount = containList.LongCount(x => x == true);

            return conditionCount / primaryCount;
        }

        public IEnumerable<List<int>> EquivalenceClassTransformation()
        {
            var transformedTransaction = new List<List<int>>();
            foreach (var li in referenceListOfItems)
                transformedTransaction.Add(new List<int>());

            for (int i = 0; i < referenceListOfItems.Count; ++i)
            {
                foreach (var c in ElementContain(referenceListOfItems[i]).Select((value, index) => new { value, index }))
                    if (c.value)
                        transformedTransaction[i].Add(c.index);
            }

            return transformedTransaction;
        }

        public void Print()
        {
            foreach (var data in dataSet.Select((value, index) => new { value, index }))
            {
                Console.Write("Id: {0}\t|\t", data.index);
                foreach (var d in data.value)
                    Console.Write(d + "\t|\t");
                Console.WriteLine();
            }
        }

        public void Print(int i)
        {
            foreach (var d in dataSet[i])
            {
                Console.Write(d + "\t|\t");
            }
            Console.WriteLine();
        }

        public void PrintIsContain(T item)
        {
            Console.Write("{0,10}:\t|\t", item);
            var isContain = ElementContain(item);
            foreach (var contain in isContain)
                Console.Write(contain + "\t|\t");
            Console.WriteLine();
        }
    }
}
