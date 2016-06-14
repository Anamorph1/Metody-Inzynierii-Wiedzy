using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AGDSDatabase.Models.AGDS
{
    public class Attribute: Node<AttributeValue>
    {
        private string columnName;

        public string ColumnName
        {
            get
            {
                return columnName;
            }
        }

        public Attribute(string ColumnName)
        {
            columnName = ColumnName;
        }

        public void FillWithAttributeValues(DatabaseModel databaseModel,string tableName)
        {
            foreach(var attrValue in databaseModel.GetAttributes(columnName,tableName))
            {
                AddNewValue(new AttributeValue(attrValue));
            }
        }



        public List<Item> GetItemsBy(object obj)
        {
            return Branches.Find(x => x.Value.ToString() == obj.ToString()).Branches;
        }
    }
}
