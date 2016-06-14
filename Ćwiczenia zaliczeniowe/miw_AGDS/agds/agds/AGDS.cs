using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace agds
{
    public sealed class AGDS
    {
        public Root Root;
        private AGDS(string inputFile)
        {
            Root = new Root(inputFile);
            Root.UpdateByItems(inputFile);
        }

        public static AGDS Create(string inputFile)
        {
            try
            {
                AGDS agds = new AGDS(inputFile);
                return agds;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception message: {0}", e);
                return null;
            }

        }

        #region UtilityMethods
        public void PrintAll()
        {
           foreach(var atr in this.Root.Nodes)
            {
                Console.WriteLine("Nazwa atrybutu: {0}", atr.Name);
                foreach(var atrVal in atr.Values)
                {
                    Console.WriteLine("Wartość: {0}",atrVal.Value);
                    foreach(var it in atrVal.Items)
                    {
                        it.Print();
                    }
                }
            }
        }

        public void PrintAll(List<Item> items)
        {
            foreach (var it in items)
            {
                it.Print();
            }
        }

        public Item GetItem(string name, object val, int index = 0)
        {
            List<Item> il = Root.Nodes.Find(x => x.Name.Equals(name)).Values.Find(x => x.Value.Equals(val)).Items ?? new List<Item>();
            if (il.Count == 1)
                return il.First();
            else if (il.Count > index)
                return il[index];
            else if (il.Count < index)
                return il.Last();
            else
                return null;
        }



        public void ResetSimilarity()
        {
            foreach (var atr in this.Root.Nodes)
            {
                foreach (var atrVal in atr.Values)
                {
                    foreach (var it in atrVal.Items)
                    {
                        it.Reset();
                    }
                }
            }
        }

        public double? GetMax(string AttributeName)
        {
            int index = Root.Nodes.FindIndex(x => x.Name == AttributeName);

            return (double?)Root.Nodes[index].Values.Max();
        }

        public double? GetMin(string AttributeName)
        {
            int index = Root.Nodes.FindIndex(x => x.Name == AttributeName);
            if (index < 0)
                return null;
            return (double?)Root.Nodes[index].Values.Min();
        }

        public double GetAvg(string AttributeName)
        {
            int index = Root.Nodes.FindIndex(x => x.Name == AttributeName);
            double avg = 0;
            if(Root.Nodes[index].Values.First().Value is double)
            {
                foreach (var attr in Root.Nodes[index].Values)
                {
                    avg += (double)attr;
                }
            }
            return avg / (double)Root.Nodes[index].Values.Count;
        }

        public double GetRange(string AttributeName)
        {
            return (this.GetMax(AttributeName) - this.GetMin(AttributeName)).GetValueOrDefault();
        }

        public List<Item> GetItemsBySimilarity(double val)
        {
            List<Item> ret = new List<Item>();
            foreach (var atr in this.Root.Nodes)
            {
                foreach (var atrVal in atr.Values)
                {
                    foreach (var it in atrVal.Items)
                    {
                        if(it.Similarity >= val)
                        {
                            if (!ret.Contains(it))
                                ret.Add(it);
                        }

                    }
                }
            }
            return ret;
        }

        public void FindSimilar(Item item)
        {
            ResetSimilarity();
            double w = 0.0;
            foreach (var atr in this.Root.Nodes.Select((value, index) => new { value, index }))
            {
                int i = atr.index;
                foreach (var atrVal in atr.value.Values)
                {
                    try
                    {
                        w = 1 - Math.Abs((double)atrVal - (double)item.Parameters[i]) / GetRange(atr.value.Name);
                    }
                    catch
                    {
                        w = 1;
                    }
                    foreach (var it in atrVal.Items)
                    {
                        if (it.Similarity == 0.0)
                            it.Similarity = w;
                        else
                            it.Similarity *= w;

                    }
                }
            }
        }


        #endregion
    }
}
