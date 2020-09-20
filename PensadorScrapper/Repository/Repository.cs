
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace PensadorScrapper.Repository
{
    class Repository
    {
        public static IDbConnection Connection()
        {
            return new SQLiteConnection(@"Data Source=banco.db;Version=3");
        }
    }
}
