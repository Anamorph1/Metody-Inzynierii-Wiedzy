using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace AGDSDatabase.Models.AGDS
{


    public class AttributeValue : Node<Item>
    {
        object value;

        public AttributeValue(object Value)
        {
            value = Value;
        }

        public object Value
        {
            get
            {
                return value;
            }
        }

        public void FillWithRecords(Table table, string columnName)
        {
            foreach(var item in table.AllRecords)
            {
                if(!Branches.Contains(item) && item.Record[columnName].Equals(value))
                {
                    Branches.Add(item);
                }
            }             
        }
    }
}
