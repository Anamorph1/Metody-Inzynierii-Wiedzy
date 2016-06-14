using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AGDSDatabase.Models
{
    interface IDatabaseModel
    {
        List<string> GetTableNames();
        List<KeyValuePair<string,Type>> GetColumnInfo(string tableName);
        List<object> GetAttributes(string columnName, string tableName);
        List<string> GetColumnNames(string tableName);
        DataRowCollection GetRecords(string tableName);
        string GetPrimaryKey(string tableName);
    }
}
