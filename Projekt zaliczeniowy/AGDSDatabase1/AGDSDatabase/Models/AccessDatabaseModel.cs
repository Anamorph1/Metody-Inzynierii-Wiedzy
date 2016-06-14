using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace AGDSDatabase.Models
{
    public class AccessDatabaseModel : DatabaseModel
    {
        private DataSet myDataSet;



        public AccessDatabaseModel(string path): base(path)
        {
            string strAccessConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;
            myDataSet = new DataSet();
            MyAccessConnection = new OleDbConnection(strAccessConn);
            List<string> tableNames =  GetTableNames();
            MyAccessConnection.Open();
            foreach(string tab in tableNames)
            {
                OleDbCommand myAccessCommand = new OleDbCommand("SELECT * FROM " + tab, MyAccessConnection);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                myDataAdapter.Fill(myDataSet, tab);
            }
            MyAccessConnection.Close();
        }

        #region IDatabaseModel
        public List<string> GetKeyNames(String tableName)
        {
            var returnList = new List<string>();


            DataTable mySchema = MyAccessConnection.
                GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys,
                                    new Object[] { null, null, tableName });


            // following is a lengthy form of the number '3' :-)
            int columnOrdinalForName = mySchema.Columns["COLUMN_NAME"].Ordinal;

            foreach (DataRow r in mySchema.Rows)
            {
                returnList.Add(r.ItemArray[columnOrdinalForName].ToString());
            }

            return returnList;
        }
        public override List<string> GetTableNames()
        {
            List<string> ret = new List<string>();
            MyAccessConnection.Open();
            DataTable dta = MyAccessConnection.GetSchema("Tables");
            string table;
            foreach (DataRow r in dta.Select("TABLE_TYPE = 'TABLE'"))
            {
                table = r["TABLE_NAME"].ToString();
                ret.Add(table);
            }
            MyAccessConnection.Close();
            return ret;
        }

        public override List<KeyValuePair<string,Type>> GetColumnInfo(string tableName)
        {
            
            List<KeyValuePair<string, Type>> ret = new List<KeyValuePair<string, Type>>();
            DataColumnCollection drc = myDataSet.Tables[tableName].Columns;
            foreach (DataColumn dc in drc)
            {
                ret.Add(new KeyValuePair<string, Type>(dc.ColumnName, dc.DataType));
            }
            return ret;
        }

        public override List<object> GetAttributes(string columnName, string tableName)
        {
            DataColumnCollection drc = myDataSet.Tables[tableName].Columns;
            int index = 0;
            foreach (DataColumn dc in drc)
            {
                if (dc.ColumnName == columnName)
                    break;
                index++;
            }
            List<object> ret = new List<object>();
            DataRowCollection dra = myDataSet.Tables[tableName].Rows;
            foreach (DataRow dr in dra)
            {
                if (ret.Contains(dr[index]))
                    continue;
                ret.Add(dr[index]);
            }
            return ret;
        }

        public override List<string> GetColumnNames(string tableName)
        {
            List<string> ret = new List<string>();
            List<KeyValuePair<string, Type>> columnInfo = GetColumnInfo(tableName);
            foreach (var column in columnInfo)
            {
                ret.Add(column.Key);
            }
            return ret;
        }

        public override DataRowCollection GetRecords(string tableName)
        {
            return myDataSet.Tables[tableName].Rows;
        }

        public override string GetPrimaryKey(string tableName)
        {
            MyAccessConnection.Open();
            List<string> tempList = new List<string>();
            DataTable mySchema = MyAccessConnection.
                GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys,
                                    new Object[] { null, null, tableName });

            int columnOrdinalForName = mySchema.Columns["COLUMN_NAME"].Ordinal;
            foreach (DataRow r in mySchema.Rows)
            {
                tempList.Add(r.ItemArray[columnOrdinalForName].ToString());
            }

            MyAccessConnection.Close();
            return tempList.First();
        }

        #endregion
    }
}
