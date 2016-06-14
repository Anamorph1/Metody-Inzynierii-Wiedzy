using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Globalization;
using miw_KNN.Resources;

namespace miw_KNN
{
    static class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Przedmiot: Metody Inżynierii Wiedzy");
            Console.WriteLine("Autor: Kamil Makarowski");
            Printers.ShowMenu();
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
                        SimpleKNN.SimpleKNN.DoKNN();
                        break;
                    case 2:
                        Console.Clear();
                        DataMinning.DataMining.DoDataMining();
                        break;
                    default:
                        break;
                }
                Console.Clear();
                Printers.ShowMenu();
            }
            Console.WriteLine("Dziękuje za skorzystanie z aplikacji");
            Console.ReadKey();
        }
    }
}
