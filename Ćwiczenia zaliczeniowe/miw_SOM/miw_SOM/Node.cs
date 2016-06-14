using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_SOM
{
    public class Node
    {
        double[] weights;
        List<Iris> objects;
        double currentDistance;
        readonly int size;
        
        public List<Iris> Objects
        {
            get
            {
                return objects;
            }
        }
        public int Size
        {
            get
            {
                return size;
            }
        }

        public double this[int i]
        {
            get
            {
                return weights[i];
            }
        }

        public double CurrentDistance
        {
            get
            {
                return currentDistance;
            }
            set
            {
                currentDistance = value;
            }
        }


        public Node()
        {
            size = 4;
            weights = new double[size];
            for (int i = 0; i < size; ++i)
            {
                weights[i] = new Random().NextDouble();
                System.Threading.Thread.Sleep(30);                
            }
            objects = new List<Iris>();
            currentDistance = 0;
        }

        public Node(double[] parameters)
        {
            size = 4;
            weights = new double[size];
            for (int i = 0; i < size; ++i)
            {
                weights[i] = parameters[i];           
            }
            objects = new List<Iris>();
            currentDistance = 0;
        }

        public void SetDistance(Iris iris)
        {
            for (int i = 0; i < weights.Count(); ++i)
            {
                currentDistance += Math.Pow(iris.Parameters[i] - weights[i], 2);
            }
            currentDistance = Math.Sqrt(currentDistance);
        }

        public void Update(int t, double delta, double gamma, Iris iris)
        {
            for(int i=0;i<size;++i)
            {
                weights[i] = weights[i] + delta * gamma * (iris.Parameters[i] - weights[i]);
            }
        }

        public void AddElement(Iris iris)
        {
            objects.Add(iris);
        }

        public void Print()
        {
            foreach(var iris in objects)
            {
                Console.Write("{0}\t", iris.Name);
            }
            Console.WriteLine();
        }

        public string PrintWeights()
        {
            string ret="";
            foreach (var w in weights)
            {
                ret += String.Format("{0:0.##} ", w);
            }
            return ret;
        }
    }
}
