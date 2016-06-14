using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using miw_KNN.Resources;

namespace miw_KNN.SimpleKNN
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
            //var outputItem = input.OrderBy(x => x);
            //foreach (var op in outputItem)
            //{
            //    Printers.Print(op as Iris);
            //    Console.WriteLine((op as Iris).Metric);
            //}


            return outputItem;
        }

        public static void DoKNN()
        {
            Console.WriteLine("Cwiczenie 1: Proste KNN");
            Console.WriteLine("Proszę wpisać wymiary testowanego Irysa");
            Console.WriteLine("długość/szerokość liścia , długość/szerokość kwiatu");
            double[] irisParams = new double[4];
            for(int i=0;i<irisParams.Length;++i)
                irisParams[i] = Converters.Convert(Console.ReadLine());

            Iris testIris = new Iris(irisParams);
            string path = @"IrisDataAll.txt";
            List<Iris> refIrisList = Iris.prepareIrisDataFromTextFile(path);
            foreach (var refIris in refIrisList)
            {
                //   Printers.Print(refIris);
                refIris.MetricToOther(testIris);
            }

            Console.WriteLine("Podaj k:");
            ushort k;
            while (!ushort.TryParse(Console.ReadLine(), out k))
                Console.WriteLine("wartość k jest błędna.");
            testIris.Name = KNN(refIrisList, k).Name;
            Console.WriteLine("Testowany Irys:");
            Printers.Print(testIris);
            if (Console.ReadLine() == "q")
                Environment.Exit(0);
        }
    }
}
