using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace miw_CrossValidation
{
    public class Iris
    {
        public double[] Parameters { get; private set; }
        public double Metric { get; private set; }
        public string Name { get; set; }

        #region Constructors
        public Iris() { }

        public Iris(double[] parameters, string name = "")
        {
            Parameters = parameters;
            Metric = 0.0;
            Name = name;
        }
        #endregion

        /*#region IComparable
        int IComparable.CompareTo(object obj)
        {
            Iris ir = obj as Iris;
            return Metric.CompareTo(ir.Metric);
        }
        #endregion*/

        public void MetricToOther(IEnumerable<Iris> irisList)
        {
            foreach(var ir in irisList)
            {
                ir.Metric = 0.0;
                for (int i = 0; i < Parameters.Length; ++i)
                    ir.Metric += Math.Pow(Parameters[i] - ir.Parameters[i], 2);
                ir.Metric = Math.Sqrt(ir.Metric);
            }
        }

        public static List<Iris> prepareIrisDataFromTextFile(string path)
        {
            List<Iris> preparedData = new List<Iris>();

            string[] records = File.ReadAllLines(path);
            double[] parameters;
            string name;

            foreach (var record in records)
            {
                string[] fields = record.Split('\t');
                name = fields.Last();
                fields = fields.Take(4).ToArray();
                parameters = Array.ConvertAll(fields, new Converter<string, double>(Convert));
                preparedData.Add(new Iris(parameters, name));
            }
            return preparedData;
        }

        public static double Convert(string input)
        {
            double value;
            Double.TryParse(input, out value);
            return value;
        }

        public void Print()
        {
            Console.WriteLine("Dł. Liścia;Sze. Liścia;Dł. Kwiatu;Sze. Kwiatu;Nazwa;Metryka:");
            foreach (var param in Parameters)
                Console.Write(param.ToString() + '\t');
            
            Console.WriteLine("{0:0.##}\t{1}", Metric, Name);
        }

        //Metoda kNN
        public string KNN(IEnumerable<Iris> input, int k)
        {
            MetricToOther(input);
            var output = input.OrderBy(x => x.Metric)
                              .Take(k)
                              .GroupBy(x => x.Name,
                                       x => x,
                                       (key, g) => new {Name = key,
                                                        ir = g.ToList()})
                              .OrderByDescending(x => x.ir.Count);
            return output.First().Name;
        }
    }
}
