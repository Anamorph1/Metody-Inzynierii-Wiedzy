using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;


namespace AGDSDatabase.Models.AGDS
{
    public class Table : Node<Attribute>
    {
        List<Item> allRecords;
        DataRowCollection dataRowCollection;
        string tableName;

        public string TableName
        {
            get
            {
                return tableName;
            }
        }

        public DataRowCollection DataRowCollection
        {
            get
            {
                return dataRowCollection;
            }
        }

        public List<Item> AllRecords
        {
            get
            {
                return allRecords;
            }
        }


        public Table(DatabaseModel databaseModel, string TableName)
        {
            allRecords = new List<Item>();
            dataRowCollection = databaseModel.GetRecords(TableName);
            foreach (DataRow dr in dataRowCollection)
            {
                allRecords.Add(new Item(dr, TableName));
            }
            tableName = TableName;
        }

        public void FillWithAttributes(DatabaseModel databaseModel)
        {
            foreach (string column in databaseModel.GetColumnNames(tableName))
            {
                AddNewValue(new Attribute(column));
            }
        }

        public Attribute GetAttribute(string name)
        {
            if (Branches.Exists(x => x.ColumnName == name))
                return Branches.Single(x => x.ColumnName == name);
            return null;
        }
    }
}
