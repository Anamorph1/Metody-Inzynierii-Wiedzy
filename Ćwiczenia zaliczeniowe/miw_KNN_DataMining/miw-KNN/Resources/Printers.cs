using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_KNN.Resources
{
    public static class Printers
    {
        public static Action<Iris> print = o => Console.WriteLine(
            "Metryka: " + o.Metric.ToString(".0##") + "\tNazwa: " + o.Name);
        


        public static void ShowMenu()
        {
            Console.WriteLine("MENU:");
            Console.WriteLine("1. Prosta metoda KNN");
            Console.WriteLine("2. Eksploracja danych");
        }

        public static void ShowDataMiningMenu()
        {
            Console.WriteLine();
            Console.WriteLine("MENU:");
            Console.WriteLine("1. Pokaż dane");
            Console.WriteLine("2. Wsparcie");
            Console.WriteLine("3. Wzorce częste");
            Console.WriteLine("4. Wsparcie (XvY)");
            Console.WriteLine("5. Pewność");
            Console.WriteLine("6. Eksploracja reguł asocjacyjnych");
            Console.WriteLine("7. Ekwiwalentna Transformacja Klas ECLAT");
        }
    }
}
