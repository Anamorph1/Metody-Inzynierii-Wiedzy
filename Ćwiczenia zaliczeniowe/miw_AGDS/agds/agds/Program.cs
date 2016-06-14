using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agds
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Przedmiot: Metody Inżynierii Wiedzy");
            Console.WriteLine("Temat: Sieć AGDS");
            Console.WriteLine("Autor: Kamil Makarowski");
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1. Zbiór irysów");
            Console.WriteLine("2. Zbiór win");
            int choice;
            string inputLine = "";
            while (inputLine != "q")
            {
                inputLine = Console.ReadLine();
                Int32.TryParse(inputLine, out choice);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        AGDS IrisAgds = AGDS.Create("IrisDataAll.txt");
                        Item IrisItem = IrisAgds.GetItem("leaf-length", 6.7);
                        if(IrisItem!=null)
                            IrisAgds.FindSimilar(IrisItem);
                        Console.Write("Irys testowany: ");
                        IrisItem.Print();
                        Console.Write("Wskaźnik podobieństwa (,): ");
                        double thresholdIris = Convert.ToDouble(Console.ReadLine());
                        List<Item> IrisSim = IrisAgds.GetItemsBySimilarity(thresholdIris);
                        Console.WriteLine("Podobieństwo z wartością >= {0}", thresholdIris);
                        foreach (var it in IrisSim)
                            it.Print();
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        AGDS WineAgds = AGDS.Create("WineDataAll.txt");
                        Item WineItem = WineAgds.GetItem("Alcohol", 13.83);
                        Console.Write("Wino testowane: ");
                        WineItem.Print();
                        WineAgds.FindSimilar(WineItem);
                        Console.Write("Wskaźnik podobieństwa (,): ");
                        double thresholdWine = Convert.ToDouble(Console.ReadLine());
                        List<Item> WineSim = WineAgds.GetItemsBySimilarity(thresholdWine);
                        Console.WriteLine("Podobieństwo z wartością >= {0}", thresholdWine);
                        foreach (var it in WineSim)
                            it.Print();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                Console.Clear();
                Console.WriteLine("\nMENU:");
                Console.WriteLine("1. Zbiór irysów");
                Console.WriteLine("2. Zbiór win");
            }
            Console.WriteLine("Dziękuje za skorzystanie z aplikacji");
            Console.ReadKey();
        }
    }
}
