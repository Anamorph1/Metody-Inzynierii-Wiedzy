using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGDSDatabase.Models;
using System.Diagnostics;
using AGDSDatabase.Models.AGDS;

namespace AGDSDatabase.ViewModels
{
    public class DatabaseViewModel : ViewModel
    {
        public DatabaseModel databaseModel;
        public AGDSModel agdsModel;
        public DatabaseViewModel(string path)
        {
            try
            {
                databaseModel = DatabaseFactory.GetFactory(path);
                if(databaseModel == null)
                {
                    Message = "Invalid database file.";
                    return;
                }
                Message = "Database has been loaded.";
                agdsModel = new AGDSModel(databaseModel);

            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
            

        }        
    }
}
