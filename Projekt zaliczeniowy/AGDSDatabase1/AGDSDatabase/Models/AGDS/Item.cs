using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AGDSDatabase.Models.AGDS
{
    public class Item
    {
        string tableName;
        DataRow record;
        List<Item> associatedRecords;
        public DataRow Record
        {
            get
            {
                return record;
            }
        }

        public List<Item> AssociatedRecords
        {
            get
            {
                return associatedRecords;
            }
            set
            {
                if (value != null)
                    associatedRecords = value;
            }
        }


        public Item(DataRow datarow,string TableName)
        {
            tableName = TableName;
            record = datarow;
            associatedRecords = new List<Item>(); 
        }

        public string Print(DatabaseModel databaseModel)
        {
            string ret = "";
            foreach (var column in databaseModel.GetColumnNames(tableName))
            {
                ret+=(record[column] + " ");
            }
            return ret;
        }

        public string Print()
        {
            return tableName.First()+record.ItemArray.First().ToString();
        }
    }
}
