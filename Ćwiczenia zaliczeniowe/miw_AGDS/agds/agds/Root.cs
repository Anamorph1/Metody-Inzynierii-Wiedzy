using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace agds
{
    public class Root
    {
        private List<Attribute> nodes;
        public List<Attribute> Nodes {
            get { return nodes; }
            private set { nodes = value; }
        }

        public Root(string inputFile)
        {
            string[] attributes = TextMethod.LineToTable(inputFile, '\t', 0);
            if(Nodes == null)
                Nodes = new List<Attribute>();
            foreach(var atr in attributes)
            {
                Nodes.Add(new Attribute(atr));
            }
        }

        public void UpdateByItems(string inputFile)
        {
            foreach (var line in File.ReadAllLines(inputFile, Encoding.GetEncoding(1250)).Skip(1))
            {
                InsertNewItem(new Item(line));
            }
        }
        public void InsertNewItem(Item item)
        {
            for(int i=0;i<item.Parameters.Count;++i)
            {
                if (!Nodes[i].CheckExist(item.Parameters[i]))
                {
                    Nodes[i].InsertAttributeValue(item.Parameters[i]);        
                }
                Nodes[i].InsertItemToAttributeValue(item.Parameters[i], item);
            }
        }
    }
}
