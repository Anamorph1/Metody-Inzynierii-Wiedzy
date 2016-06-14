using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGDSDatabase.Models.AGDS
{
    public class AGDSModel
    {
        private Root root;

        public AGDSModel(DatabaseModel database)
        {
            root = new Root(database);
        }

        public List<Table> GetTables()
        {
            return root.Branches;
        }

        public List<Attribute> GetAttributes(Table table)
        {
            List<Attribute> ret = new List<Attribute>();
            if (table == null)
                return ret;
            foreach(var attr in table.Branches)
            {
                ret.Add(attr);
            }
            return ret;
        }

        public List<AttributeValue> GetAttributeValues(Attribute attribute)
        {
            List<AttributeValue> ret = new List<AttributeValue>();
            if (attribute == null)
                return new List<AttributeValue>();
            foreach (var attrVal in attribute.Branches)
            {
                ret.Add(attrVal);
            }
            return ret;
        }


        public List<Item> GetItems(AttributeValue attributeValue)
        {
            List<Item> ret = new List<Item>();
            foreach (var item in attributeValue.Branches)
            {
                ret.Add(item);
            }
            return ret;
        }

        public List<Item> GetAssociatedItems(Item item)
        {
            List<Item> ret = new List<Item>();
            foreach (var it in item.AssociatedRecords)
            {
                ret.Add(it);
            }
            return ret;
        }

        public List<Item> GetItemsByExpr(string expr)
        {
            
            if (expr.Contains("="))
            {
                string[] e = expr.Split('=');
                string[] ef;
                if(e.First().Contains("."))
                {
                    ef = e.First().Split('.');
                    Table table = root.Branches.Find(x => x.TableName == ef.First());
                    AttributeValue attrVal = GetAttributeValues(GetAttributes(table).
                        Find(x => x.ColumnName == ef.Last())).
                        Find(x => x.Value.ToString() == e.Last());
                    if(attrVal == null)
                        return new List<Item>();
                    List<Item> ret = new List<Item>();
                    foreach(var br in attrVal.Branches)
                    {
                        foreach (var b in br.AssociatedRecords)
                            ret.Add(b);
                    }
                    return ret;
                }
                else
                {
                    Table table = root.Branches.Find(x => x.TableName == "Studenci");
                    AttributeValue attrVal = GetAttributeValues(GetAttributes(table).
                        Find(x => x.ColumnName == e.First())).
                        Find(x => x.Value.ToString() == e.Last());
                    if (attrVal == null)
                        return new List<Item>();
                    return attrVal.Branches;
                }
            }
            else
                return new List<Item>();
        }

    }
}
