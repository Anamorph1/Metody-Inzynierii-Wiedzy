using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace miw_SOM
{
    public class Iris
    {
        public double[] Parameters { get; private set; }
        public string Name { get; set; }

        public Iris() { }

        public Iris(double[] parameters, string name = "")
        {
            Parameters = parameters;
            Name = name;
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

        public void Print()
        {
            foreach(var p in Parameters)
            {
                Console.Write("{0}\t", p);
            }
            Console.Write(Name);
            Console.WriteLine();
        }

    }
}
