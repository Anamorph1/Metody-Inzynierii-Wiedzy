using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_SOM
{
    public class SOM
    {

        readonly int sigma0;
        readonly int alfa;
        readonly int gamma0;

        Network network;

        public SOM(int size, int sigma, int gamma, int alfa, List<Iris> li)
        {
            sigma0 = sigma;
            gamma0 = gamma;
            this.alfa = alfa;
            network = new Network(size, li);
        }

        private double GetSigma(int t)
        {
            double td = t;
            double alfad = alfa;
            return sigma0 * Math.Exp((-td / alfad));
        }

        private double GetGamma(int t)
        {
            double td = t;
            double alfad = alfa;
            return gamma0 * Math.Exp(-td / alfad);
        }

        public void Print()
        {
            for(int i=0;i<network.Size;++i)
            {
                for (int j = 0; j < network.Size; ++j)
                {
                    Console.Write("{0:N4}\t", network[i, j].CurrentDistance);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }

        public void PrintWithIris()
        {
            Point point = new Point();
            for (int i = 0; i < network.Size; ++i)
            {
                for (int j = 0; j < network.Size; ++j)
                {
                    point.SetPoint(i, j);
                    string p = point.Print();
                    Console.WriteLine(p +"\t"+ network[i, j].PrintWeights());

                    network[i, j].Print();             
                }
                Console.WriteLine();
            }
        }

        private double GetDelta(Point winner, Point looser, int t, WebType type)
        {
            return Math.Exp(-Math.Pow(GetDistance(winner, looser,type), 2) / (2 * Math.Pow(GetSigma(t), 2)));
        }

        private double GetDistance(Point winner, Point looser, WebType type)
        {
            if (type == WebType.square)
                return Math.Abs(winner.X - looser.X) + Math.Abs(winner.Y - looser.Y);
            else
                return Math.Sqrt(Math.Pow(winner.X - looser.X, 2) + Math.Pow(winner.Y - looser.Y, 2));
        }

        public void WeightUpdate(int t, Iris iris)
        {
            double gammat = GetGamma(t);

            network.SetDistance(iris);
            Point winner = network.GetCoordinatesOfMinimumDistance();
            Point looser = new Point();
            for (int i = 0; i < network.Size; ++i)
            {
                for (int j = 0; j < network.Size; ++j)
                {
                    looser.SetPoint(i, j);
                    double deltat = GetDelta(winner, looser, t, WebType.square);
                    network[i, j].Update(t, deltat, gammat, iris);
                }
            }
        }

        public void AssignIris(List<Iris> iristList)
        {
            foreach (var iris in iristList)
            {
                network.InsertIris(iris);
            }
        }
    }
}
