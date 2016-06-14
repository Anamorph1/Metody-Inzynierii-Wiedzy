using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;

namespace AGDSDatabase.Models
{
    public abstract class DatabaseModel : INotifyPropertyChanged, IDatabaseModel
    {
        private string path;
        private OleDbConnection myAccessConnection;

        public OleDbConnection MyAccessConnection
        {
            get
            {
                return myAccessConnection;
            }
            set
            {
                myAccessConnection = value;
            }
        }
        public string Path
        {
            get
            {
                return path;
            }
            private set
            {
                path = value;
            }
        }

        public DatabaseModel(string path)
        {
            Path = path;
        }

        #region INOtifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region IDatabaseModel
        public abstract List<string> GetTableNames();
        public abstract List<KeyValuePair<string, Type>> GetColumnInfo(string tableName);
        public abstract List<object> GetAttributes(string columnName, string tableName);
        public abstract List<string> GetColumnNames(string tableName);
        public abstract DataRowCollection GetRecords(string tableName);
        public abstract string GetPrimaryKey(string tableName);
        #endregion
    }
}
