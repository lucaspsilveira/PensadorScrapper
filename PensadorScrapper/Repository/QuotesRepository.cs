using Dapper;
using PensadorScrapper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PensadorScrapper.Repository
{
    class QuotesRepository : Repository
    {
        public static void SaveQuote(Quote quote)
        {
            using (IDbConnection connection = Connection() )
            {
                connection.Execute("INSERT OR IGNORE INTO quotes (id, autor, texto) values (@Id, @Autor, @Texto)", quote);
            }
        }
    }
}
