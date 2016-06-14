using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGDSDatabase.Models
{
    static class DatabaseType
    {
        public enum DBType
        {
            MicrosoftAccess,
            MicrosoftSQL,
            Oracle,
            OtherType
        }

        public static DBType GetType(string path)
        {
            if (path.EndsWith(".accdb"))
            {
                return DBType.MicrosoftAccess;
            }
            else
                return DBType.OtherType;
        }
    }
}
