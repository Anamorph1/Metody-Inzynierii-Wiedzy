using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace miw_CrossValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            File.Delete("log.txt");
            string path = @"IrisDataAll.txt";
            List<Iris> refIrisList = Iris.prepareIrisDataFromTextFile(path);
            List<Iris> temporaryList = new List<Iris>();
            int it = 30;
            int numLearnPatterns = 15;
            int choice = 0;
            int index = 0;
            List<double> avgPercentage = Enumerable.Repeat(0.0, refIrisList.Count - numLearnPatterns).ToList();
            Console.WriteLine("Walidacja krzyżowa:");
            Console.WriteLine("1. Losowe");
            Console.WriteLine("2. Losowe (proporcjonalnie)");
            choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Wybrano losowe");
                for (int i = 0; i < it; ++i)
                {
                    Random rand = new Random();
                    System.Threading.Thread.Sleep(50);
                    Console.WriteLine("Iteracja: " + i);
                    while (temporaryList.Count < numLearnPatterns)
                    {
                        index = rand.Next(refIrisList.Count);
                        temporaryList.Add(refIrisList[index]);
                        refIrisList.Remove(refIrisList[index]);
                    }

                    for (int k = 1; k < refIrisList.Count; ++k)
                    {
                        int correct = 0;
                        foreach (var tl in temporaryList)
                        {
                            string name = tl.KNN(refIrisList, k);
                            if (name.Equals(tl.Name))
                                ++correct;
                        }
                        avgPercentage[k - 1] += (double)correct / (double)temporaryList.Count * 100;
                        double percentageCorrect = (double)correct / (double)temporaryList.Count * 100;
                        //Console.WriteLine("Parametr k={1} / Procent: {0:0.##}", percentageCorrect, k);
                    }
                    temporaryList.Clear();
                    refIrisList = Iris.prepareIrisDataFromTextFile(path);
                }
            }
            else
            {
                Console.WriteLine("Wybrano losowe (proporcjonalne");
                for (int i = 0; i < it; ++i)
                {
                    Random rand = new Random();
                    System.Threading.Thread.Sleep(50);
                    Console.WriteLine("Iteracja: " + i);
                    for(int pat=0;pat<3;++pat)
                    {
                        int v = 1;
                        while (v <= numLearnPatterns/3)
                        {
                            index = rand.Next(pat*(50-pat*numLearnPatterns/3),(pat+1)*50-(pat*numLearnPatterns/3) - v);
                            temporaryList.Add(refIrisList[index]);
                            refIrisList.Remove(refIrisList[index]);
                            ++v;
                        }
                    }
                    for (int k = 1; k < refIrisList.Count; ++k)
                    {
                        int correct = 0;
                        foreach (var tl in temporaryList)
                        {
                            string name = tl.KNN(refIrisList, k);
                            if (name.Equals(tl.Name))
                                ++correct;
                        }
                        avgPercentage[k - 1] += (double)correct / (double)temporaryList.Count * 100;
                        double percentageCorrect = (double)correct / (double)temporaryList.Count * 100;
                        //Console.WriteLine("Parametr k={1} / Procent: {0:0.##}", percentageCorrect, k);
                    }
                    temporaryList.Clear();
                    refIrisList = Iris.prepareIrisDataFromTextFile(path);
                }
            }

            for (int j = 0; j < avgPercentage.Count; ++j)
            {
                avgPercentage[j] /= it;
                File.AppendAllText("log.txt", (j + 1) + "\t" + avgPercentage[j] + Environment.NewLine);
                Console.WriteLine("Parametr k={1} / Procent: {0:0.##}", avgPercentage[j], j+1);
            }
            Console.Read();
        }
    }

}
