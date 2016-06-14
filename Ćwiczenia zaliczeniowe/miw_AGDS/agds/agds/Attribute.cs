using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agds
{
    public class Attribute
    {
        private string name;
        private List<AttributeValue> values;

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }       
         }
        public List<AttributeValue> Values
        {
            get
            {
                return values;
            }
            private set
            {
                values = value;
            }
        }

        public Attribute(string name)
        {
            this.Name = name;
            if (Values == null)
                Values = new List<AttributeValue>();
        }

        public bool CheckExist(object value)
        { 
            foreach (var val in Values)
            {
                if (val.Value.Equals(value))
                    return true;
            }
            return false;
        }

        public void InsertAttributeValue(object value)
        {
            Values.Add(new AttributeValue(value));
            if (value is double)
                Values.Sort();
        }

        public void InsertItemToAttributeValue(object value,Item item)
        {

            int index = Values.FindIndex(x => x.Value.Equals(value));
            Values[index].AddItem(item);
        }

    }
}
