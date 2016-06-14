using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGDSDatabase.Models
{
    abstract class DatabaseFactory
    {

        public static DatabaseModel GetFactory(string path)
        {
            switch (DatabaseType.GetType(path))
            {
                case DatabaseType.DBType.MicrosoftAccess:
                    return new AccessDatabaseModel(path);
                default:
                    return null;             
            }

        }
    }
}
