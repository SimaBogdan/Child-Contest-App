using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

//using Mono.Data.Sqlite;
namespace src.DBUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string,string> props)
        {
            String connectionString = props["ConnectionString"];
            Console.WriteLine("SQLite ---Se deschide o conexiune la  ... {0}", connectionString);
            return new SqliteConnection(connectionString);
        }
    }
}