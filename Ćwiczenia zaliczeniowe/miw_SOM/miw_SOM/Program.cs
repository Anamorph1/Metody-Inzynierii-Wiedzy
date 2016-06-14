using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_SOM
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"IrisDataAll.txt";
            List<Iris> refIrisList = Iris.prepareIrisDataFromTextFile(path);
            List<Iris> temporaryList = new List<Iris>();
            SOM som = new SOM(7, 5, 1, 1000, refIrisList);
            int numOfIteration = 3000;
            int index = 0;
            Random rand = new Random();
            som.Print();
            for (int i = 0; i < numOfIteration; ++i)
            {
                Console.WriteLine("Numer iteracji: {0}", i);
                int j = 0;
                while (refIrisList.Count != 0)
                {
                    index = rand.Next(refIrisList.Count);
                    som.WeightUpdate(j, refIrisList[index]);
                    temporaryList.Add(refIrisList[index]);
                    refIrisList.Remove(refIrisList[index]);
                    //som.Print();
                    ++j;
                }
                refIrisList = new List<Iris>(temporaryList);
                temporaryList.Clear();
                //som.Print();

            }
            som.AssignIris(refIrisList);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            som.PrintWithIris();









            Console.ReadLine();
        }
    }
}
