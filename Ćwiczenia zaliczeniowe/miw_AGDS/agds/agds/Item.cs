using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agds
{
    public class Item
    {
        public List<object> Parameters { get; }
        public double similarity;

        public double Similarity
        {
            get;
            set;
        }

        public Item(string line)
        {
            similarity = 0.0;
            string[] parameters = TextMethod.LineToTable(line, '\t');
            if (Parameters == null)
                Parameters = new List<object>();
            foreach (var p in parameters)
            {
                if (p.ToDouble() != null)
                    Parameters.Add(p.ToDouble());
                else
                    Parameters.Add(p);
            }
        }

        public Item(string[] parameters)
        {
            if (Parameters == null)
                Parameters = new List<object>();
            foreach (var p in parameters)
            {
                if (p.ToDouble() != null)
                    Parameters.Add(p.ToDouble());
                else
                    Parameters.Add(p);
            }
        }

        public void Print()
        {
            foreach(var p in Parameters)
            {
                Console.Write("{0}\t",p);
            }
            Console.Write(Similarity);
            Console.WriteLine();
        }

        public void Reset()
        {
            similarity = 0.0;
        }
    }
}
