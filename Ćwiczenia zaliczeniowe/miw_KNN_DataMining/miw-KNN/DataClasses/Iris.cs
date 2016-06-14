using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using miw_KNN.Resources;

namespace miw_KNN
{
    public class Iris : IComparable
    {
        public double[] Parameters { get; private set; }
        public double Metric { get; private set; }
        public string Name { get; set; }

        public Iris() { }

        public Iris(double[] parameters, string name = "")
        {
            Parameters = parameters;
            Metric = 0.0;
            Name = name;
        }

        public void MetricToOther(Iris iris)
        {
            for (int i = 0; i < this.Parameters.Length; ++i)
                Metric += Math.Pow(Parameters[i]-iris.Parameters[i], 2);
            Metric = Math.Sqrt(Metric);
        }

        int IComparable.CompareTo(object obj)
        {
            Iris ir = obj as Iris;
            return this.Metric.CompareTo(ir.Metric);
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
                parameters = Array.ConvertAll(fields, new Converter<string, double>(Converters.Convert));
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

    }
}
