using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.IO;

using MySql.Data;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ldvdbdal
{
    public class DbConnectionFactory : IConnectionFactory
    {

        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;        

        public DbConnectionFactory(string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException("connectionName");

            JObject settingsFileJson = JObject.Parse(File.ReadAllText("ldsettings.json"));
            var connectionstringssettings = settingsFileJson["ConnectionStrings"];
            var connectionString = connectionstringssettings[connectionName].ToObject<string>();

            _provider = MySql.Data.MySqlClient.MySqlClientFactory.Instance;
            _connectionString = connectionString;
        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
