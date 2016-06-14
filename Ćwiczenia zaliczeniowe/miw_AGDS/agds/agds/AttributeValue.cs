using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agds
{
    public class AttributeValue : IComparable
    {
        public object val;
        public List<Item> items;
        public object Value
        {
            get
            {
                return val;
            }
            private set
            {
                val = value;
            }
        }

        public List<Item> Items
        {
            get
            {
                return items;
            }
            private set
            {
                items = value;
            }
        }
       
        public AttributeValue()
        {
            if(Items == null)
                Items = new List<Item>();
        }

        public AttributeValue(object value)
        {
            Value = value;
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public int CompareTo(object obj)
        {
            AttributeValue attr = obj as AttributeValue;
            double val1_ = (double)attr.Value;
            double val2_ = (double)this.Value;
            return val2_.CompareTo(val1_);
        }

        public static explicit operator double(AttributeValue attr)
        {
            try
            {
                return (double)attr.Value;
            }
            catch(Exception e)
            {
                return 0.0;
            }

        }
    }
}
