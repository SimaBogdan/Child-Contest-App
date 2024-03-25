using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
//using Mono.Data.Sqlite;
using ConnectionFactory = src.DBUtils.ConnectionFactory;

//using System.Data.SQLite;

//using Mono.Data.Sqlite;
namespace src.DBUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string,string> props)
        {
            var connectionString = props["ConnectionString"];
            return new SQLiteConnection(connectionString);
        }
    }
}