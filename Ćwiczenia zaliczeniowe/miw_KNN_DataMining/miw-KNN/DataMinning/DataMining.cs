using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using miw_KNN.Resources;
using System.IO;

namespace miw_KNN.DataMinning
{
    public static class DataMining
    {
        public static void DoDataMining()
        {
            string path = @"Transactions.txt";
            string[] records = File.ReadAllLines(path);
            List<string> referenceItems = new List<string>
            { "kawa", "mleko", "cukier",
                "orzechy", "jajka", "chleb",
                "maslo", "miod", "platki", "ser"
            }; 
            TransactionSet<string> transactions = new TransactionSet<string>(referenceItems);
            List<string> tempList = new List<string>();
            foreach (var record in records)
            {
                string[] fields = record.Split('\t');
                foreach (var field in fields)
                {
                    tempList.Add(field);
                }
                transactions.Add(tempList);
                tempList.Clear();
            }

            Console.WriteLine("Cwiczenie 2: Eksploracja danych");
            int choice;
            string line = "";
            do
            {
                Printers.ShowDataMiningMenu();
                line = Console.ReadLine();
                Console.Clear();
                Int32.TryParse(line, out choice);
                switch (choice)
                {
                    case 1:
                        transactions.Print();
                        break;
                    case 2:
                        foreach (var item in referenceItems)
                            Console.WriteLine("Przedmiot: {0,10} | {1:P2}", item, transactions.Support(item));
                        break;
                    case 3:
                        double thold;
                        Console.Write("Próg (ułamek ','): ");
                        Double.TryParse(Console.ReadLine(), out thold);
                        var frequentELements = transactions.Frequent(thold);
                        foreach (var item in frequentELements)
                            Console.WriteLine("Przedmiot: {0,10} | {1:P2}", item, transactions.Support(item));
                        break;
                    case 4:
                        Console.Write("Przedmiot 1: ");
                        string item1 = Console.ReadLine();
                        Console.Write("Przedmiot 2: ");
                        string item2 = Console.ReadLine();
                        double orCond = transactions.ConditionSupportOr(item1, item2);
                        Console.WriteLine("Przedmiot 1: {0}, Przedmiot 2: {1}, Wsparcie warunkowe: {2:P2}",item1,item2,orCond );
                        break;
                    case 5:
                        Console.Write("Przedmiot 1: ");
                        item1 = Console.ReadLine();
                        Console.Write("Przedmiot 2: ");
                        item2 = Console.ReadLine();
                        Console.WriteLine("{0} -> {1} {2:P2}",item1,item2,transactions.ConditionConfidence(item1, item2));
                        break;
                    case 6:
                        Console.Write("Próg (ułamek ','): ");
                        Double.TryParse(Console.ReadLine(), out thold);
                        foreach (var it1 in referenceItems)
                        {
                            
                            foreach(var it2 in referenceItems)
                            {
                                if (it1 == it2)
                                {
                                    continue;
                                } 
                                else
                                {
                                   double or =  transactions.ConditionSupportOr(it1, it2);
                                   double conf =  transactions.ConditionConfidence(it1, it2);
                                   if (or >= thold && conf >= thold)
                                   {
                                        Console.WriteLine("{0} -> {1} ({2:P2},{3:P2})", it1, it2, or, conf);
                                   }
                                        
                                }

                            }
                        }
                        break;
                    case 7:
                        var EquivalenceClass = transactions.EquivalenceClassTransformation();
                        foreach(var eq in EquivalenceClass.Select((value,index) => new { value, index }))
                        {
                            Console.Write("\n{0,10}\t|",transactions.ReferenceListOfItems[eq.index]); 
                            foreach(var e in eq.value)
                            {
                                Console.Write("\t{0}\t|",e);
                            }
                        }
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
                if (line == "q")
                    Environment.Exit(0);
            } while (!String.IsNullOrWhiteSpace(line));
        }
    }
}
