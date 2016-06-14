using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

namespace AGDSDatabase.Models.AGDS
{
    class Root : Node<Table>
    {
        public Root(DatabaseModel databaseModel)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            FillWithTables(databaseModel);
            #region test
            foreach( var table in Branches)
            {
                table.FillWithAttributes(databaseModel);
                foreach(var attribute in table.Branches)
                {
                    attribute.FillWithAttributeValues(databaseModel, table.TableName);
                    foreach(var attrValue in attribute.Branches)
                    {
                        attrValue.FillWithRecords(table, attribute.ColumnName);
                        
                   }                  
                }
            }
            FillAssociated(databaseModel);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Debug.WriteLine(elapsedMs);


            #endregion
        }

        public void FillWithTables(DatabaseModel databaseModel)
        {
            foreach(string table in databaseModel.GetTableNames())
            {
                    AddNewValue(new Table(databaseModel,table));
            }
        }

        public bool IsConnected(DatabaseModel databaseModel, Table Source, Table Destination)
        {
            if (Source.TableName == Destination.TableName)
                return false;

            string primaryKey = databaseModel.GetPrimaryKey(Source.TableName);
            List<string> columnNames = databaseModel.GetColumnNames(Destination.TableName);
            if (columnNames.Contains(primaryKey))
                return true;
            return false;
        }

        public void FillAssociated(DatabaseModel databaseModel)
        {
            foreach(var t1 in Branches)
            {
                foreach(var t2 in Branches)
                {
                    if (IsConnected(databaseModel, t1, t2))
                    {
                        Attribute attribute = t1.GetAttribute(databaseModel.GetPrimaryKey(t1.TableName));
                        foreach (var attr in t2.Branches)
                        {
                            foreach (var attrVal in attr.Branches)
                            {
                                foreach (var record in attrVal.Branches)
                                {                                   
                                    if (attr.ColumnName.Equals(attribute.ColumnName))
                                    {
                                        foreach(var assoc in attribute.GetItemsBy(attrVal.Value))
                                        {
                                            if (!record.AssociatedRecords.Contains(assoc))
                                            {
                                                record.AssociatedRecords.Add(assoc);
                                                assoc.AssociatedRecords.Add(record);
                                            }          
                                        }
                                    }

                                }
                                    
                            }
                         }
                    }
                }
            }
        }
    }

}
