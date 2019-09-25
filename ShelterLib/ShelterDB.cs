
using SportingAppFW.Data.Common.DB.Sqlite;


namespace ShelterLib
{
    public class ShelterDB: SaSQLiteDBClass
    {
        private static ShelterDB instance =null;
        private ShelterDB() : base("DisShelter.db") { }

        private static object _lock = new object();
        public static ShelterDB Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        return new ShelterDB();
                    }
                    else
                        return instance;
                }
            }
        }

    }
}


