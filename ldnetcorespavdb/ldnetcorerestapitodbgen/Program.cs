using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using MySql.Data.MySqlClient;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ldnetcorerestapitodbgen
{
    class Program
    {
        static void Main(string[] args)
        {
            var isWindows = true;
            var settingsjsonname = "settings.json";

            var basePath = "";
            var fileprefix = "ldncgen";

            if (isWindows == true)
                settingsjsonname += ".windows";
            else
                settingsjsonname += ".mac";

            JObject settingsFileJson = JObject.Parse(File.ReadAllText(settingsjsonname));

            // see if we can connect to the database

            MySqlConnection connection = new MySqlConnection(settingsFileJson["mariadbconnstring"].ToObject<String>());

            try
            {
                connection.Open();

                // see if have any tables to pull

                var listTables = new List<string>();
                var mapTablesToColunms = new Dictionary<string, List<Column>>();

                using (MySqlCommand command = new MySqlCommand("SELECT  TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'yivvdb'", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listTables.Add((string)reader["TABLE_NAME"]);
                        }
                    }
                }

                if (0 < listTables.Count)
                {
                    // load up table columns
                    foreach (var table in listTables)
                    {
                        using (MySqlCommand command = new MySqlCommand("SELECT  * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND TABLE_SCHEMA = 'yivvdb'", connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                mapTablesToColunms.Add(table, new List<Column>());

                                while (reader.Read())
                                {
                                    mapTablesToColunms[table].Add(new Column { Name = (string)reader["COLUMN_NAME"], columnType = ColumnType.String });

                                    switch (reader["DATA_TYPE"])
                                    {
                                        case "bigint":
                                            mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].columnType = ColumnType.Long;
                                            break;
                                        case "varchar":
                                            mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].columnType = ColumnType.String;
                                            break;
                                        case "datetime":
                                            mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].columnType = ColumnType.DateTime;
                                            break;
                                        case "tinyint":
                                            mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].columnType = ColumnType.Long;
                                            break;
                                        case "decimal":
                                            mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].columnType = ColumnType.Numeric;
                                            break;
                                        case "mediumtext":
                                            mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].columnType = ColumnType.String;
                                            break;
                                        case "longblob":
                                            mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].columnType = ColumnType.Blob;
                                            break;
                                        default:
                                            throw new Exception("type not found -> " + reader["DATA_TYPE"]);
                                            break;
                                    }

                                    if (0 == string.Compare(Convert.ToString(reader["IS_NULLABLE"]), "YES"))
                                    {
                                        mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].AllowsNull = true;
                                    }
                                    else
                                    {
                                        mapTablesToColunms[table][mapTablesToColunms[table].Count - 1].AllowsNull = false;
                                    }

                                }
                            }
                        }
                    }

                    // now build up our pathing for file output

                    basePath = settingsFileJson["outputproject"].ToObject<String>();

                    var classlibraryPath = basePath + Path.DirectorySeparatorChar + "classlibrary";
                    var businesslogicPath = basePath + Path.DirectorySeparatorChar + "businesslogic";
                    var dalPath = basePath + Path.DirectorySeparatorChar + "dal";
                    var webapispath = basePath + Path.DirectorySeparatorChar + "webapis";
                    var javascriptpath = basePath + Path.DirectorySeparatorChar + "wwwroot" + Path.DirectorySeparatorChar + "js";

                    var classlibrarynamespace = settingsFileJson["classlibrarynamespace"].ToObject<String>();
                    var dalnamespace = settingsFileJson["dalnamespace"].ToObject<String>();
                    var blnamespace = settingsFileJson["blnamespace"].ToObject<String>();
                    var classlibraryoutputproject = settingsFileJson["classlibraryoutputproject"].ToObject<String>();
                    var dataaccessoutputproject = settingsFileJson["dataaccessoutputproject"].ToObject<String>();
                    var businesslogicoutputproject = settingsFileJson["businesslogicoutputproject"].ToObject<String>();

                    // delete all previous output (what if a table was deleted, this needs to be full cycle without leaving deleted table objects)

                    var listFilesToDelete = new List<String>();

                    listFilesToDelete.AddRange(Directory.GetFiles(basePath, fileprefix + "*"));                                    
                    listFilesToDelete.AddRange(Directory.GetFiles(webapispath, fileprefix + "*"));
                    listFilesToDelete.AddRange(Directory.GetFiles(javascriptpath, fileprefix + "*"));
                    listFilesToDelete.AddRange(Directory.GetFiles(classlibraryoutputproject, fileprefix + "*"));
                    listFilesToDelete.AddRange(Directory.GetFiles(dataaccessoutputproject, fileprefix + "*"));
                    listFilesToDelete.AddRange(Directory.GetFiles(businesslogicoutputproject, fileprefix + "*"));

                    foreach (var fileToDeletePath in listFilesToDelete)
                    {
                        File.Delete(fileToDeletePath);
                    }

                    // start generation                    

                    // output classlibrary files

                    // enumerations

                    var fileEnumsContentsBuilder = new StringBuilder();

                    fileEnumsContentsBuilder.AppendLine("using System;");
                    fileEnumsContentsBuilder.AppendLine();
                    fileEnumsContentsBuilder.AppendLine("namespace " + classlibrarynamespace);
                    fileEnumsContentsBuilder.AppendLine("{");
                    fileEnumsContentsBuilder.AppendLine("   public enum ControllerProtocols : Int64");
                    fileEnumsContentsBuilder.AppendLine("   {");

                    fileEnumsContentsBuilder.AppendLine("       StartTransaction = 100,");
                    fileEnumsContentsBuilder.AppendLine("       CommittTransaction = 101,");
                    fileEnumsContentsBuilder.AppendLine("       RollbackTransaction = 102,");

                    var protocolCounter = 1000;

                    foreach (var tablename in listTables)
                    {
                        fileEnumsContentsBuilder.AppendLine("       Create" + tablename + " = " + protocolCounter.ToString() + ",");
                        protocolCounter += 1000;
                        fileEnumsContentsBuilder.AppendLine("       Update" + tablename + " = " + protocolCounter.ToString() + ",");
                        protocolCounter += 1000;
                        fileEnumsContentsBuilder.AppendLine("       Delete" + tablename + " = " + protocolCounter.ToString() + ",");
                        protocolCounter += 1000;
                        fileEnumsContentsBuilder.AppendLine("       RetrieveByID" + tablename + " = " + protocolCounter.ToString() + ",");
                        protocolCounter += 1000;
                        fileEnumsContentsBuilder.AppendLine("       RetrieveWithWhereClause" + tablename + " = " + protocolCounter.ToString() + ",");
                        protocolCounter += 1000;
                    }

                    fileEnumsContentsBuilder.AppendLine("   }");
                    fileEnumsContentsBuilder.AppendLine("}");                    

                    // write class library project
                    File.WriteAllText(classlibraryoutputproject + Path.DirectorySeparatorChar + fileprefix + "enumerations" + ".cs", fileEnumsContentsBuilder.ToString());

                    // classes

                    foreach (var tablename in listTables)
                    {
                        var fileContentsBuilder = new StringBuilder();

                        fileContentsBuilder.AppendLine("using System;");
                        fileContentsBuilder.AppendLine();
                        fileContentsBuilder.AppendLine("namespace " + classlibrarynamespace);
                        fileContentsBuilder.AppendLine("{");
                        fileContentsBuilder.AppendLine("    public class " + tablename);
                        fileContentsBuilder.AppendLine("    {");

                        foreach (var column in mapTablesToColunms[tablename])
                        {
                            fileContentsBuilder.Append("        public ");

                            switch (column.columnType)
                            {
                                case ColumnType.DateTime:
                                    fileContentsBuilder.Append("DateTime");

                                    if (column.AllowsNull == true)
                                        fileContentsBuilder.Append("? ");
                                    else
                                        fileContentsBuilder.Append(" ");
                                    
                                    break;
                                case ColumnType.Long:
                                    fileContentsBuilder.Append("long");

                                    if (column.AllowsNull == true)
                                        fileContentsBuilder.Append("? ");
                                    else
                                        fileContentsBuilder.Append(" ");

                                    break;
                                case ColumnType.String:
                                    fileContentsBuilder.Append("string ");
                                    break;
                                case ColumnType.Numeric:
                                    fileContentsBuilder.Append("decimal");

                                    if (column.AllowsNull == true)
                                        fileContentsBuilder.Append("? ");
                                    else
                                        fileContentsBuilder.Append(" ");

                                    break;
                                case ColumnType.Blob:
                                    fileContentsBuilder.Append("byte[] ");
                                    break;
                            }                            

                            fileContentsBuilder.Append(column.Name);
                            fileContentsBuilder.Append(" { get; set; } ");
                            fileContentsBuilder.AppendLine("");
                        }

                        fileContentsBuilder.AppendLine("    }");
                        fileContentsBuilder.AppendLine("}");                        

                        // write class library project
                        File.WriteAllText(classlibraryoutputproject + Path.DirectorySeparatorChar + fileprefix + tablename + ".cs", fileContentsBuilder.ToString());
                    }

                    // output repos

                    foreach (var tablename in listTables)
                    {
                        var fileContentsBuilder = new StringBuilder();

                        fileContentsBuilder.AppendLine("using System;");
                        fileContentsBuilder.AppendLine("using System.Collections.Generic;");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("using " + classlibrarynamespace + ";");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("namespace " + dalnamespace);
                        fileContentsBuilder.AppendLine("{");
                        fileContentsBuilder.AppendLine("    public interface I" + tablename + "Repository");
                        fileContentsBuilder.AppendLine("    {");
                        fileContentsBuilder.AppendLine("        long Create(" + tablename + " new" + tablename + ");");
                        fileContentsBuilder.AppendLine("        int Update(" + tablename + " existing" + tablename + ");");
                        fileContentsBuilder.AppendLine("        int Delete(" + tablename + " existing" + tablename + ");");
                        fileContentsBuilder.AppendLine("        " + tablename + " RetrieveByID(long existing" + tablename + "id);");
                        fileContentsBuilder.AppendLine("        List<" + tablename + "> " + "RetrieveWithWhereClause" + tablename + "(string WhereClause);");
                        fileContentsBuilder.AppendLine("    }");
                        fileContentsBuilder.AppendLine("}");

                        // write to data access project
                        File.WriteAllText(dataaccessoutputproject + Path.DirectorySeparatorChar + fileprefix + "I" + tablename + "Repository.cs", fileContentsBuilder.ToString());
                    }

                    foreach (var tablename in listTables)
                    {
                        var fileContentsBuilder = new StringBuilder();

                        fileContentsBuilder.AppendLine("using System;");
                        fileContentsBuilder.AppendLine("using System.Collections.Generic;");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("using System.Data;");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("using " + classlibrarynamespace + ";");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("namespace " + dalnamespace);
                        fileContentsBuilder.AppendLine("{");
                        fileContentsBuilder.AppendLine("    public class " + tablename + "Repository : Repository<" + tablename + ">, I" + tablename + "Repository");
                        fileContentsBuilder.AppendLine("    {");
                        fileContentsBuilder.AppendLine("        private DbContext _context;");
                        fileContentsBuilder.AppendLine("        public " + tablename + "Repository(DbContext context)");
                        fileContentsBuilder.AppendLine("            : base(context)");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            _context = context;");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public long Create(" + tablename + " new" + tablename + ")");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            using (var command = _context.CreateCommand())");
                        fileContentsBuilder.AppendLine("            {");
                        fileContentsBuilder.AppendLine("                command.CommandType = CommandType.Text;");

                        fileContentsBuilder.Append("                command.CommandText = \"INSERT INTO " + tablename + " (");

                        for (int i = 1; i < mapTablesToColunms[tablename].Count; i++)
                        {
                            fileContentsBuilder.Append(mapTablesToColunms[tablename][i].Name);

                            if (i < (mapTablesToColunms[tablename].Count - 1))
                            {
                                fileContentsBuilder.Append(", ");
                            }
                        }

                        fileContentsBuilder.Append(") VALUES (");

                        for (int i = 1; i < mapTablesToColunms[tablename].Count; i++)
                        {
                            fileContentsBuilder.Append("@" + mapTablesToColunms[tablename][i].Name);

                            if (i < (mapTablesToColunms[tablename].Count - 1))
                            {
                                fileContentsBuilder.Append(", ");
                            }
                        }

                        fileContentsBuilder.AppendLine(")\";");

                        for (int i = 1; i < mapTablesToColunms[tablename].Count; i++)
                        {
                            fileContentsBuilder.AppendLine("                var " + mapTablesToColunms[tablename][i].Name + "param = command.CreateParameter();");
                            fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][i].Name + "param.ParameterName = \"@" + mapTablesToColunms[tablename][i].Name + "\";");
                            fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][i].Name + "param.Value = new" + tablename + "." + mapTablesToColunms[tablename][i].Name + ";");
                            fileContentsBuilder.AppendLine("                command.Parameters.Add(" + mapTablesToColunms[tablename][i].Name + "param);");
                        }

                        fileContentsBuilder.AppendLine("                command.ExecuteNonQuery();");
                        fileContentsBuilder.AppendLine("                command.CommandText = \"SELECT LAST_INSERT_ID(); \";");
                        fileContentsBuilder.AppendLine("                return Convert.ToInt64(command.ExecuteScalar());");

                        fileContentsBuilder.AppendLine("            }");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public int Update(" + tablename + " existing" + tablename + ")");
                        fileContentsBuilder.AppendLine("        {");

                        fileContentsBuilder.AppendLine("            using (var command = _context.CreateCommand())");
                        fileContentsBuilder.AppendLine("            {");
                        fileContentsBuilder.AppendLine("                command.CommandType = CommandType.Text;");

                        fileContentsBuilder.Append("                command.CommandText = \"UPDATE " + tablename + " SET ");

                        for (int i = 1; i < mapTablesToColunms[tablename].Count; i++)
                        {
                            fileContentsBuilder.Append(mapTablesToColunms[tablename][i].Name + " = @" + mapTablesToColunms[tablename][i].Name);

                            if (i < (mapTablesToColunms[tablename].Count - 1))
                            {
                                fileContentsBuilder.Append(", ");
                            }
                        }

                        fileContentsBuilder.Append(" WHERE " + mapTablesToColunms[tablename][0].Name + " = @" + mapTablesToColunms[tablename][0].Name);
                        fileContentsBuilder.AppendLine("\";");

                        for (int i = 0; i < mapTablesToColunms[tablename].Count; i++)
                        {
                            fileContentsBuilder.AppendLine("                var " + mapTablesToColunms[tablename][i].Name + "param = command.CreateParameter();");
                            fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][i].Name + "param.ParameterName = \"@" + mapTablesToColunms[tablename][i].Name + "\";");
                            fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][i].Name + "param.Value = existing" + tablename + "." + mapTablesToColunms[tablename][i].Name + ";");
                            fileContentsBuilder.AppendLine("                command.Parameters.Add(" + mapTablesToColunms[tablename][i].Name + "param);");
                        }

                        fileContentsBuilder.AppendLine("                return command.ExecuteNonQuery();");

                        fileContentsBuilder.AppendLine("            }");

                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public int Delete(" + tablename + " existing" + tablename + ")");
                        fileContentsBuilder.AppendLine("        {");

                        fileContentsBuilder.AppendLine("            using (var command = _context.CreateCommand())");
                        fileContentsBuilder.AppendLine("            {");
                        fileContentsBuilder.AppendLine("                command.CommandType = CommandType.Text;");

                        fileContentsBuilder.AppendLine("                command.CommandText = \"DELETE FROM " + tablename + " WHERE " + mapTablesToColunms[tablename][0].Name + " = @" + mapTablesToColunms[tablename][0].Name + "\";");

                        fileContentsBuilder.AppendLine("                var " + mapTablesToColunms[tablename][0].Name + "param = command.CreateParameter();");
                        fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][0].Name + "param.ParameterName = \"@" + mapTablesToColunms[tablename][0].Name + "\";");
                        fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][0].Name + "param.Value = existing" + tablename + "." + mapTablesToColunms[tablename][0].Name + ";");
                        fileContentsBuilder.AppendLine("                command.Parameters.Add(" + mapTablesToColunms[tablename][0].Name + "param);");

                        fileContentsBuilder.AppendLine("                return command.ExecuteNonQuery();");

                        fileContentsBuilder.AppendLine("            }");

                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public " + tablename + " RetrieveByID(long existing" + tablename + "id)");
                        fileContentsBuilder.AppendLine("        {");

                        fileContentsBuilder.AppendLine("            " + tablename + " ret = null;");

                        fileContentsBuilder.AppendLine("            using (var command = _context.CreateCommand())");
                        fileContentsBuilder.AppendLine("            {");
                        fileContentsBuilder.AppendLine("                command.CommandType = CommandType.Text;");

                        fileContentsBuilder.AppendLine("                command.CommandText = \"SELECT * FROM " + tablename + " WHERE " + mapTablesToColunms[tablename][0].Name + " = @" + mapTablesToColunms[tablename][0].Name + "\";");

                        fileContentsBuilder.AppendLine("                var " + mapTablesToColunms[tablename][0].Name + "param = command.CreateParameter();");
                        fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][0].Name + "param.ParameterName = \"@" + mapTablesToColunms[tablename][0].Name + "\";");
                        fileContentsBuilder.AppendLine("                " + mapTablesToColunms[tablename][0].Name + "param.Value = existing" + tablename + "id;");
                        fileContentsBuilder.AppendLine("                command.Parameters.Add(" + mapTablesToColunms[tablename][0].Name + "param);");

                        fileContentsBuilder.AppendLine("                var reader = command.ExecuteReader();");

                        fileContentsBuilder.AppendLine("                while (reader.Read())");
                        fileContentsBuilder.AppendLine("                {");
                        fileContentsBuilder.AppendLine("                     ret = new " + tablename + "();");

                        for (int i = 0; i < mapTablesToColunms[tablename].Count; i++)
                        {                           
                            switch (mapTablesToColunms[tablename][i].columnType)
                            {
                                case ColumnType.String:
                                    fileContentsBuilder.Append("                     ret." + mapTablesToColunms[tablename][i].Name + " = reader.");

                                    if (mapTablesToColunms[tablename][i].AllowsNull == true)                                    
                                        fileContentsBuilder.AppendLine("IsDBNull(" + i.ToString() + ") ? null : reader.GetString(" + i.ToString() + ");");                                    
                                    else                                    
                                        fileContentsBuilder.AppendLine("GetString(" + i.ToString() + ");");                                                                       
                                    break;
                                case ColumnType.Long:
                                    fileContentsBuilder.Append("                     ret." + mapTablesToColunms[tablename][i].Name + " = reader.");

                                    if (mapTablesToColunms[tablename][i].AllowsNull == true)
                                        fileContentsBuilder.AppendLine("IsDBNull(" + i.ToString() + ") ? (long?)null : reader.GetInt64(" + i.ToString() + ");");
                                    else
                                        fileContentsBuilder.AppendLine("GetInt64(" + i.ToString() + ");");
                                    break;
                                case ColumnType.DateTime:
                                    fileContentsBuilder.Append("                     ret." + mapTablesToColunms[tablename][i].Name + " = reader.");                                    

                                    if (mapTablesToColunms[tablename][i].AllowsNull == true)
                                        fileContentsBuilder.AppendLine("IsDBNull(" + i.ToString() + ") ? (DateTime?)null : reader.GetDateTime(" + i.ToString() + ");");
                                    else
                                        fileContentsBuilder.AppendLine("GetDateTime(" + i.ToString() + ");");
                                    break;
                                case ColumnType.Numeric:
                                    fileContentsBuilder.Append("                     ret." + mapTablesToColunms[tablename][i].Name + " = reader.");                                    

                                    if (mapTablesToColunms[tablename][i].AllowsNull == true)
                                        fileContentsBuilder.AppendLine("IsDBNull(" + i.ToString() + ") ? (decimal?)null : reader.GetDecimal(" + i.ToString() + ");");
                                    else
                                        fileContentsBuilder.AppendLine("GetDecimal(" + i.ToString() + ");");
                                    break;
                                case ColumnType.Blob:
                                    fileContentsBuilder.AppendLine("                     byte[] buffer = new byte[200000000];");
                                    fileContentsBuilder.Append("                     long read = reader.");
                                    fileContentsBuilder.AppendLine("GetBytes(" + i.ToString() + ", 0, buffer, 0, 2000000000);");
                                    fileContentsBuilder.AppendLine("                     ret." + mapTablesToColunms[tablename][i].Name + " = new byte[read];");
                                    fileContentsBuilder.AppendLine("                     Buffer.BlockCopy(buffer, 0, ret." + mapTablesToColunms[tablename][i].Name + ", 0, Convert.ToInt32(read));");
                                    break;
                            }
                            
                        }

                        fileContentsBuilder.AppendLine("                }");

                        fileContentsBuilder.AppendLine("                return ret;");

                        fileContentsBuilder.AppendLine("            }");

                        fileContentsBuilder.AppendLine("        }");

                        fileContentsBuilder.AppendLine("        public List<" + tablename + "> RetrieveWithWhereClause" + tablename + "(string WhereClause)");
                        fileContentsBuilder.AppendLine("        {");

                        fileContentsBuilder.AppendLine("            var ret = new List<" + tablename + ">();");

                        fileContentsBuilder.AppendLine("            using (var command = _context.CreateCommand())");
                        fileContentsBuilder.AppendLine("            {");
                        fileContentsBuilder.AppendLine("                command.CommandType = CommandType.Text;");

                        fileContentsBuilder.AppendLine("                command.CommandText = \"SELECT * FROM " + tablename + " WHERE \" + WhereClause;");

                        fileContentsBuilder.AppendLine("                var reader = command.ExecuteReader();");

                        fileContentsBuilder.AppendLine("                while (reader.Read())");
                        fileContentsBuilder.AppendLine("                {");
                        fileContentsBuilder.AppendLine("                     var itemtoadd = new " + tablename + "();");

                        for (int i = 0; i < mapTablesToColunms[tablename].Count; i++)
                        {
                            switch (mapTablesToColunms[tablename][i].columnType)
                            {
                                case ColumnType.String:
                                    fileContentsBuilder.Append("                     itemtoadd." + mapTablesToColunms[tablename][i].Name + " = reader.");
                                    fileContentsBuilder.AppendLine("GetString(" + i.ToString() + ");");
                                    break;
                                case ColumnType.Long:
                                    fileContentsBuilder.Append("                     itemtoadd." + mapTablesToColunms[tablename][i].Name + " = reader.");
                                    fileContentsBuilder.AppendLine("GetInt64(" + i.ToString() + ");");
                                    break;
                                case ColumnType.DateTime:
                                    fileContentsBuilder.Append("                     itemtoadd." + mapTablesToColunms[tablename][i].Name + " = reader.");
                                    fileContentsBuilder.AppendLine("GetDateTime(" + i.ToString() + ");");
                                    break;
                                case ColumnType.Numeric:
                                    fileContentsBuilder.Append("                     itemtoadd." + mapTablesToColunms[tablename][i].Name + " = reader.");
                                    fileContentsBuilder.AppendLine("GetDecimal(" + i.ToString() + ");");
                                    break;
                                case ColumnType.Blob:
                                    fileContentsBuilder.AppendLine("                     byte[] buffer = new byte[200000000];");
                                    fileContentsBuilder.Append("                     long read = reader.");
                                    fileContentsBuilder.AppendLine("GetBytes(" + i.ToString() + ", 0, buffer, 0, 2000000000);");
                                    fileContentsBuilder.AppendLine("                     itemtoadd." + mapTablesToColunms[tablename][i].Name + " = new byte[read];");
                                    fileContentsBuilder.AppendLine("                     Buffer.BlockCopy(buffer, 0, itemtoadd." + mapTablesToColunms[tablename][i].Name + ", 0, Convert.ToInt32(read));");
                                    break;
                            }                                
                        }

                        fileContentsBuilder.AppendLine("                    ret.Add(itemtoadd);");

                        fileContentsBuilder.AppendLine("                }");

                        fileContentsBuilder.AppendLine("                return ret;");

                        fileContentsBuilder.AppendLine("            }");

                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("    }");
                        fileContentsBuilder.AppendLine("}");

                        File.WriteAllText(dataaccessoutputproject + Path.DirectorySeparatorChar + fileprefix + tablename + "Repository.cs", fileContentsBuilder.ToString());
                    }

                    // output business logic

                    foreach (var tablename in listTables)
                    {
                        var fileContentsBuilder = new StringBuilder();

                        fileContentsBuilder.AppendLine("using System;");
                        fileContentsBuilder.AppendLine("using System.Collections.Generic;");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("using " + classlibrarynamespace + ";");
                        fileContentsBuilder.AppendLine("using " + dalnamespace + ";");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("namespace " + blnamespace);
                        fileContentsBuilder.AppendLine("{");
                        fileContentsBuilder.AppendLine("    public class " + tablename + "Manager : I" + tablename + "Manager");
                        fileContentsBuilder.AppendLine("    {");
                        fileContentsBuilder.AppendLine("        private DbContext _dbContext = null;");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("        private " + tablename + "Repository AllocateRepo()");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            " + tablename + "Repository repo = new " + tablename + "Repository(_dbContext);");
                        fileContentsBuilder.AppendLine("            return repo;");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public " + tablename + "Manager(DbContext _dbContext)");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            this._dbContext = _dbContext;");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public long Create(" + tablename + " new" + tablename + ")");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            return AllocateRepo().Create(new" + tablename + ");");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public int Update(" + tablename + " existing" + tablename + ")");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            return AllocateRepo().Update(existing" + tablename + ");");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public int Delete(" + tablename + " existing" + tablename + ")");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            return AllocateRepo().Delete(existing" + tablename + ");");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public " + tablename + " RetrieveByID(long existing" + tablename + "id)");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            return AllocateRepo().RetrieveByID(existing" + tablename + "id);");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("        public List<" + tablename + "> RetrieveWithWhereClause" + tablename + "(string WhereClause)");
                        fileContentsBuilder.AppendLine("        {");
                        fileContentsBuilder.AppendLine("            return AllocateRepo().RetrieveWithWhereClause" + tablename + "(WhereClause);");
                        fileContentsBuilder.AppendLine("        }");
                        fileContentsBuilder.AppendLine("    }");
                        fileContentsBuilder.AppendLine("}");

                        File.WriteAllText(businesslogicoutputproject + Path.DirectorySeparatorChar + fileprefix + tablename + "Manager.cs", fileContentsBuilder.ToString());
                    }

                    foreach (var tablename in listTables)
                    {
                        var fileContentsBuilder = new StringBuilder();

                        fileContentsBuilder.AppendLine("using System;");
                        fileContentsBuilder.AppendLine("using System.Collections.Generic;");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("using " + classlibrarynamespace + ";");
                        fileContentsBuilder.AppendLine("");
                        fileContentsBuilder.AppendLine("namespace " + blnamespace);
                        fileContentsBuilder.AppendLine("{");
                        fileContentsBuilder.AppendLine("    public interface I" + tablename + "Manager");
                        fileContentsBuilder.AppendLine("    {");
                        fileContentsBuilder.AppendLine("        long Create(" + tablename + " new" + tablename + ");");
                        fileContentsBuilder.AppendLine("        int Update(" + tablename + " existing" + tablename + ");");
                        fileContentsBuilder.AppendLine("        int Delete(" + tablename + " existing" + tablename + ");");
                        fileContentsBuilder.AppendLine("        " + tablename + " RetrieveByID(long existing" + tablename + "id);");
                        fileContentsBuilder.AppendLine("        List<" + tablename + "> RetrieveWithWhereClause" + tablename + "(string WhereClause);");
                        fileContentsBuilder.AppendLine("    }");
                        fileContentsBuilder.AppendLine("}");

                        File.WriteAllText(businesslogicoutputproject + Path.DirectorySeparatorChar + fileprefix + "I" + tablename + "Manager.cs", fileContentsBuilder.ToString());
                    }

                    // output web apis

                    var fileContentsBuilderWebApi = new StringBuilder();

                    fileContentsBuilderWebApi.AppendLine("using System;");
                    fileContentsBuilderWebApi.AppendLine("using System.Collections.Generic;");
                    fileContentsBuilderWebApi.AppendLine("using System.Linq;");
                    fileContentsBuilderWebApi.AppendLine("using System.Threading.Tasks;");
                    fileContentsBuilderWebApi.AppendLine("using Microsoft.AspNetCore.Http;");
                    fileContentsBuilderWebApi.AppendLine("using Microsoft.AspNetCore.Mvc;");
                    fileContentsBuilderWebApi.AppendLine("using System.Dynamic;");
                    fileContentsBuilderWebApi.AppendLine("using Microsoft.Extensions.Caching.Memory; ");
                    fileContentsBuilderWebApi.AppendLine("using System.IO; ");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("using Newtonsoft;");
                    fileContentsBuilderWebApi.AppendLine("using Newtonsoft.Json;");
                    fileContentsBuilderWebApi.AppendLine("using Newtonsoft.Json.Converters;");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("using " + classlibrarynamespace + ";");
                    fileContentsBuilderWebApi.AppendLine("using " + dalnamespace + ";");
                    fileContentsBuilderWebApi.AppendLine("using " + blnamespace + "; ");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("namespace netcoreldspaframework.WebApis");
                    fileContentsBuilderWebApi.AppendLine("{");
                    fileContentsBuilderWebApi.AppendLine("    public class ProtocolReturnPacket");
                    fileContentsBuilderWebApi.AppendLine("    {");
                    fileContentsBuilderWebApi.AppendLine("        public bool succeeded { get; set; }");
                    fileContentsBuilderWebApi.AppendLine("        public dynamic payload { get; set; }");
                    fileContentsBuilderWebApi.AppendLine("    }");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("    [Produces(\"application/json\")]");
                    fileContentsBuilderWebApi.AppendLine("    [Route(\"api/Main\")]");
                    fileContentsBuilderWebApi.AppendLine("    public class MainController : Controller");
                    fileContentsBuilderWebApi.AppendLine("    {");
                    fileContentsBuilderWebApi.AppendLine("        private IMemoryCache cache;");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("        public MainController(IMemoryCache cache)");
                    fileContentsBuilderWebApi.AppendLine("        {");
                    fileContentsBuilderWebApi.AppendLine("            this.cache = cache;");
                    fileContentsBuilderWebApi.AppendLine("        }");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("        // POST: api/Main");
                    fileContentsBuilderWebApi.AppendLine("        [HttpPost]");
                    fileContentsBuilderWebApi.AppendLine("        public string Post()");
                    fileContentsBuilderWebApi.AppendLine("        {");
                    fileContentsBuilderWebApi.AppendLine("            var protocolReturnPacket = new ProtocolReturnPacket();");
                    fileContentsBuilderWebApi.AppendLine("            protocolReturnPacket.succeeded = false;");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("            try");
                    fileContentsBuilderWebApi.AppendLine("            {");
                    fileContentsBuilderWebApi.AppendLine("                var stream = new StreamReader(Request.Body);");
                    fileContentsBuilderWebApi.AppendLine("                var body = stream.ReadToEnd();");             
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("                var converter = new ExpandoObjectConverter();");
                    fileContentsBuilderWebApi.AppendLine("                dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(body, converter);");
                    fileContentsBuilderWebApi.AppendLine("                var expandoDict = (IDictionary<string, object>)obj;");
                    fileContentsBuilderWebApi.AppendLine("                var protocol = expandoDict[\"protocol\"];");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("                switch ((ControllerProtocols)protocol)");
                    fileContentsBuilderWebApi.AppendLine("                {");
                    fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.StartTransaction: // start tran");
                    fileContentsBuilderWebApi.AppendLine("                        {");
                    fileContentsBuilderWebApi.AppendLine("                            var transactionId = Guid.NewGuid().ToString();");
                    fileContentsBuilderWebApi.AppendLine("                            var _context = new DbContext(new DbConnectionFactory(\"DefaultConnection\"));");
                    fileContentsBuilderWebApi.AppendLine("                            var _uow = _context.CreateUnitOfWork();");
                    fileContentsBuilderWebApi.AppendLine("                            cache.Set<DbContext>(\"dbcontext-\" + transactionId, _context);");
                    fileContentsBuilderWebApi.AppendLine("                            cache.Set<IUnitOfWork>(\"uow-\" + transactionId, _uow);");
                    fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.payload = transactionId;");
                    fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.succeeded = true;");
                    fileContentsBuilderWebApi.AppendLine("                        }");
                    fileContentsBuilderWebApi.AppendLine("                        break;");
                    fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.CommittTransaction: // commit tran");
                    fileContentsBuilderWebApi.AppendLine("                        {");
                    fileContentsBuilderWebApi.AppendLine("                            string transactionId = obj.payload;");
                    fileContentsBuilderWebApi.AppendLine("                            var _uow = (IUnitOfWork)cache.Get(\"uow-\" + transactionId);");
                    fileContentsBuilderWebApi.AppendLine("                            _uow.SaveChanges();");
                    fileContentsBuilderWebApi.AppendLine("                            _uow.Dispose();");
                    fileContentsBuilderWebApi.AppendLine("                            cache.Remove(\"dbcontext-\" + transactionId);");
                    fileContentsBuilderWebApi.AppendLine("                            cache.Remove(\"uow-\" + transactionId);");
                    fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.succeeded = true;");
                    fileContentsBuilderWebApi.AppendLine("                        }");
                    fileContentsBuilderWebApi.AppendLine("                        break;");
                    fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.RollbackTransaction: // rollback tran");
                    fileContentsBuilderWebApi.AppendLine("                        {");
                    fileContentsBuilderWebApi.AppendLine("                            string transactionId = obj.payload;");
                    fileContentsBuilderWebApi.AppendLine("                            var _uow = (IUnitOfWork)cache.Get(\"uow-\" + transactionId);");
                    fileContentsBuilderWebApi.AppendLine("                            _uow.Dispose();");
                    fileContentsBuilderWebApi.AppendLine("                            cache.Remove(\"dbcontext-\" + transactionId);");
                    fileContentsBuilderWebApi.AppendLine("                            cache.Remove(\"uow-\" + transactionId);");
                    fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.succeeded = true;");
                    fileContentsBuilderWebApi.AppendLine("                        }");
                    fileContentsBuilderWebApi.AppendLine("                        break;");
                    
                    foreach (var tablename in listTables)
                    {
                        fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.Create" + tablename + ":");
                        fileContentsBuilderWebApi.AppendLine("                        {");
                        fileContentsBuilderWebApi.AppendLine("                            var transactionid = expandoDict[\"transactionid\"];");
                        fileContentsBuilderWebApi.AppendLine("                            " + tablename + "Manager manager = null;");
                        fileContentsBuilderWebApi.AppendLine("                            DbContext _context = null;");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (null != transactionid)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(cache.Get<DbContext>(\"dbcontext-\" + transactionid));");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                 _context = new DbContext(new DbConnectionFactory(\"DefaultConnection\"));");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(_context);");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var new" + tablename + " = new " + tablename + "();");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var payloadexpandodict = (IDictionary<string, object>)expandoDict[\"payload\"];");
                        fileContentsBuilderWebApi.AppendLine("");

                        for (int i = 1; i < mapTablesToColunms[tablename].Count; i++) // skip unique key
                        {
                            fileContentsBuilderWebApi.Append("                            new" + tablename + "." + mapTablesToColunms[tablename][i].Name + " = ");

                            switch(mapTablesToColunms[tablename][i].columnType)
                            {
                                case ColumnType.String:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToString(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.Long:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToInt64(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.DateTime:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToDateTime(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.Numeric:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToDecimal(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.Blob:
                                    fileContentsBuilderWebApi.AppendLine("(byte[])(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                            }                            
                        }
                        
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var newobjectid = manager.Create(new" + tablename + ");");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.succeeded = true;");
                        fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.payload = newobjectid;");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (transactionid == null)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                _context.Dispose();");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                        }");
                        fileContentsBuilderWebApi.AppendLine("                        break;");

                        protocolCounter += 1000;

                        fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.Update" + tablename + ":");
                        fileContentsBuilderWebApi.AppendLine("                        {");
                        fileContentsBuilderWebApi.AppendLine("                            var transactionid = expandoDict[\"transactionid\"];");
                        fileContentsBuilderWebApi.AppendLine("                            " + tablename + "Manager manager = null;");
                        fileContentsBuilderWebApi.AppendLine("                            DbContext _context = null;");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (null != transactionid)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(cache.Get<DbContext>(\"dbcontext-\" + transactionid));");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                 _context = new DbContext(new DbConnectionFactory(\"DefaultConnection\"));");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(_context);");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var existing" + tablename + " = new " + tablename + "();");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var payloadexpandodict = (IDictionary<string, object>)expandoDict[\"payload\"];");
                        fileContentsBuilderWebApi.AppendLine("");

                        for (int i = 0; i < mapTablesToColunms[tablename].Count; i++) 
                        {
                            fileContentsBuilderWebApi.Append("                            existing" + tablename + "." + mapTablesToColunms[tablename][i].Name + " = ");

                            switch (mapTablesToColunms[tablename][i].columnType)
                            {
                                case ColumnType.String:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToString(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.Long:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToInt64(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.DateTime:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToDateTime(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.Numeric:
                                    fileContentsBuilderWebApi.AppendLine("Convert.ToDecimal(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                                case ColumnType.Blob:
                                    fileContentsBuilderWebApi.AppendLine("(byte[])(payloadexpandodict[\"" + mapTablesToColunms[tablename][i].Name + "\"]);");
                                    break;
                            }
                        }

                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var rowUpdateCount = manager.Update(existing" + tablename + ");");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (rowUpdateCount == 1)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = true;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = false;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (transactionid == null)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                _context.Dispose();");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                        }");
                        fileContentsBuilderWebApi.AppendLine("                        break;");

                        protocolCounter += 1000;

                        fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.Delete" + tablename + ":");
                        fileContentsBuilderWebApi.AppendLine("                        {");
                        fileContentsBuilderWebApi.AppendLine("                            var transactionid = expandoDict[\"transactionid\"];");
                        fileContentsBuilderWebApi.AppendLine("                            " + tablename + "Manager manager = null;");
                        fileContentsBuilderWebApi.AppendLine("                            DbContext _context = null;");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (null != transactionid)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(cache.Get<DbContext>(\"dbcontext-\" + transactionid));");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                 _context = new DbContext(new DbConnectionFactory(\"DefaultConnection\"));");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(_context);");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var existing" + tablename + " = new " + tablename + "();");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var payloadexpandodict = (IDictionary<string, object>)expandoDict[\"payload\"];");
                        fileContentsBuilderWebApi.AppendLine("");                        
                        fileContentsBuilderWebApi.Append("                            existing" + tablename + "." + mapTablesToColunms[tablename][0].Name + " = ");                          
                        fileContentsBuilderWebApi.AppendLine("Convert.ToInt64(payloadexpandodict[\"" + mapTablesToColunms[tablename][0].Name + "\"]);");                          
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var rowUpdateCount = manager.Delete(existing" + tablename + ");");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (rowUpdateCount == 1)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = true;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = false;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (transactionid == null)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                _context.Dispose();");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                        }");
                        fileContentsBuilderWebApi.AppendLine("                        break;");

                        protocolCounter += 1000;

                        fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.RetrieveByID" + tablename + ":");
                        fileContentsBuilderWebApi.AppendLine("                        {");
                        fileContentsBuilderWebApi.AppendLine("                            var transactionid = expandoDict[\"transactionid\"];");
                        fileContentsBuilderWebApi.AppendLine("                            " + tablename + "Manager manager = null;");
                        fileContentsBuilderWebApi.AppendLine("                            DbContext _context = null;");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (null != transactionid)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(cache.Get<DbContext>(\"dbcontext-\" + transactionid));");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                 _context = new DbContext(new DbConnectionFactory(\"DefaultConnection\"));");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(_context);");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var existing" + tablename + "id = Convert.ToInt64(expandoDict[\"payload\"]);");                        
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existing" + tablename + "id));");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (null != protocolReturnPacket.payload)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = true;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = false;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (transactionid == null)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                _context.Dispose();");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                        }");
                        fileContentsBuilderWebApi.AppendLine("                        break;");

                        protocolCounter += 1000;

                        fileContentsBuilderWebApi.AppendLine("                    case ControllerProtocols.RetrieveWithWhereClause" + tablename + ":");
                        fileContentsBuilderWebApi.AppendLine("                        {");
                        fileContentsBuilderWebApi.AppendLine("                            var transactionid = expandoDict[\"transactionid\"];");
                        fileContentsBuilderWebApi.AppendLine("                            " + tablename + "Manager manager = null;");
                        fileContentsBuilderWebApi.AppendLine("                            DbContext _context = null;");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (null != transactionid)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(cache.Get<DbContext>(\"dbcontext-\" + transactionid));");
                        fileContentsBuilderWebApi.AppendLine("                             }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                _context = new DbContext(new DbConnectionFactory(\"DefaultConnection\"));");
                        fileContentsBuilderWebApi.AppendLine("                                manager = new " + tablename + "Manager(_context);");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            var WhereClause = Convert.ToString(expandoDict[\"payload\"]);");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            protocolReturnPacket.payload = manager.RetrieveWithWhereClause" + tablename + "(WhereClause);");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (null != protocolReturnPacket.payload)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = true;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                            else");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                protocolReturnPacket.succeeded = false;");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("");
                        fileContentsBuilderWebApi.AppendLine("                            if (transactionid == null)");
                        fileContentsBuilderWebApi.AppendLine("                            {");
                        fileContentsBuilderWebApi.AppendLine("                                _context.Dispose();");
                        fileContentsBuilderWebApi.AppendLine("                            }");
                        fileContentsBuilderWebApi.AppendLine("                        }");
                        fileContentsBuilderWebApi.AppendLine("                        break;");

                        protocolCounter += 1000;
                    }

                    fileContentsBuilderWebApi.AppendLine("                }");

                    fileContentsBuilderWebApi.AppendLine("            }");
                    fileContentsBuilderWebApi.AppendLine("            catch (Exception ex)");
                    fileContentsBuilderWebApi.AppendLine("            {");
                    fileContentsBuilderWebApi.AppendLine("                // might want to do something with this in the future.");
                    fileContentsBuilderWebApi.AppendLine("            }");
                    fileContentsBuilderWebApi.AppendLine("");
                    fileContentsBuilderWebApi.AppendLine("            return JsonConvert.SerializeObject(protocolReturnPacket);");

                    fileContentsBuilderWebApi.AppendLine("        }");
                    fileContentsBuilderWebApi.AppendLine("    }");
                    fileContentsBuilderWebApi.AppendLine("}");

                    File.WriteAllText(webapispath + Path.DirectorySeparatorChar + fileprefix + "Controller.cs", fileContentsBuilderWebApi.ToString());

                    // output javascript client

                    var fileContentsBuilderJavascript = new StringBuilder();

                    fileContentsBuilderJavascript.AppendLine("");
                    fileContentsBuilderJavascript.AppendLine("/// <reference path =\"../../node_modules/@types/jquery/index.d.ts\"/>");
                    fileContentsBuilderJavascript.AppendLine("");

                    fileContentsBuilderJavascript.AppendLine("var eStartTransaction = 100;");
                    fileContentsBuilderJavascript.AppendLine("var eCommittTransaction = 101;");
                    fileContentsBuilderJavascript.AppendLine("var eRollbackTransaction = 102;");

                    protocolCounter = 1000;

                    foreach (var tablename in listTables)
                    {
                        fileContentsBuilderJavascript.AppendLine("var " + "eCreate" + tablename + " = " + protocolCounter.ToString() + ";");
                        protocolCounter += 1000;
                        fileContentsBuilderJavascript.AppendLine("var " + "eUpdate" + tablename + " = " + protocolCounter.ToString() + ";");
                        protocolCounter += 1000;
                        fileContentsBuilderJavascript.AppendLine("var " + "eDelete" + tablename + " = " + protocolCounter.ToString() + ";");
                        protocolCounter += 1000;
                        fileContentsBuilderJavascript.AppendLine("var " + "eRetrieveByID" + tablename + " = " + protocolCounter.ToString() + ";");
                        protocolCounter += 1000;
                        fileContentsBuilderJavascript.AppendLine("var " + "eRetrieveWithWhereClause" + tablename + " = " + protocolCounter.ToString() + ";");
                        protocolCounter += 1000;
                    }

                    fileContentsBuilderJavascript.AppendLine("");

                    foreach (var tablename in listTables)
                    {
                        fileContentsBuilderJavascript.AppendLine("class " + tablename);
                        fileContentsBuilderJavascript.AppendLine("{");

                        foreach (var column in mapTablesToColunms[tablename])
                        {
                            fileContentsBuilderJavascript.Append("    " + column.Name);

                            switch (column.columnType)
                            {
                                case ColumnType.DateTime:
                                    fileContentsBuilderJavascript.Append(": Date; ");
                                    break;
                                case ColumnType.Long:
                                    fileContentsBuilderJavascript.Append(": number; ");
                                    break;
                                case ColumnType.String:
                                    fileContentsBuilderJavascript.Append(": string; ");
                                    break;
                                case ColumnType.Numeric:
                                    fileContentsBuilderJavascript.Append(": number; ");
                                    break;
                                case ColumnType.Blob:
                                    fileContentsBuilderJavascript.Append(": any; ");
                                    break;
                            }

                            fileContentsBuilderJavascript.AppendLine("");
                        }

                        fileContentsBuilderJavascript.AppendLine("}");
                        fileContentsBuilderJavascript.AppendLine("");
                    }

                    fileContentsBuilderJavascript.AppendLine("");
                    fileContentsBuilderJavascript.AppendLine("class ProtocolPacket");
                    fileContentsBuilderJavascript.AppendLine("{");
                    fileContentsBuilderJavascript.AppendLine("    protocol: number;");
                    fileContentsBuilderJavascript.AppendLine("    transactionid: string;");
                    fileContentsBuilderJavascript.AppendLine("    payload: any;");
                    fileContentsBuilderJavascript.AppendLine("}");
                    fileContentsBuilderJavascript.AppendLine("");
                    fileContentsBuilderJavascript.AppendLine("class AjajClient");
                    fileContentsBuilderJavascript.AppendLine("{");
                    fileContentsBuilderJavascript.AppendLine("");
                    fileContentsBuilderJavascript.AppendLine("    private postAjaj(objectToSend, cb)");
                    fileContentsBuilderJavascript.AppendLine("    {");
                    fileContentsBuilderJavascript.AppendLine("");
                    fileContentsBuilderJavascript.AppendLine("        $.ajax({");
                    fileContentsBuilderJavascript.AppendLine("                type: \"POST\",");
                    fileContentsBuilderJavascript.AppendLine("                url: 'api/Main',");
                    fileContentsBuilderJavascript.AppendLine("                data: JSON.stringify(objectToSend),");
                    fileContentsBuilderJavascript.AppendLine("                contentType: \"application/json; charset=utf-8\", ");
                    fileContentsBuilderJavascript.AppendLine("                dataType: \"json\",");
                    fileContentsBuilderJavascript.AppendLine("                timeout: 500000,");
                    fileContentsBuilderJavascript.AppendLine("                success: function(data) {");
                    fileContentsBuilderJavascript.AppendLine("                     cb(data)");
                    fileContentsBuilderJavascript.AppendLine("                }");                    
                    fileContentsBuilderJavascript.AppendLine("        });");
                    fileContentsBuilderJavascript.AppendLine("    };");                    
                    fileContentsBuilderJavascript.AppendLine("");

                    fileContentsBuilderJavascript.AppendLine("    StartTransaction(cb) {");
                    fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                    fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eStartTransaction;");
                    fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = null;");
                    fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                    fileContentsBuilderJavascript.AppendLine("    }");

                    fileContentsBuilderJavascript.AppendLine("");

                    fileContentsBuilderJavascript.AppendLine("    CommitTransaction(transactionId: string, cb) {");
                    fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                    fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eCommittTransaction;");
                    fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = transactionId;");
                    fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                    fileContentsBuilderJavascript.AppendLine("    }");

                    fileContentsBuilderJavascript.AppendLine("");

                    fileContentsBuilderJavascript.AppendLine("    RollbackTransaction(transactionId: string, cb) {");
                    fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                    fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eRollbackTransaction;");
                    fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = transactionId;");
                    fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                    fileContentsBuilderJavascript.AppendLine("    }");

                    fileContentsBuilderJavascript.AppendLine("");                   

                    foreach (var tablename in listTables)
                    {
                        fileContentsBuilderJavascript.AppendLine("    Create" + tablename + "(new" + tablename + ": " + tablename + ", transactionId: string, cb)");
                        fileContentsBuilderJavascript.AppendLine("    {");
                        fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eCreate" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.transactionid = transactionId;");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = new" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                        fileContentsBuilderJavascript.AppendLine("    }");
                        fileContentsBuilderJavascript.AppendLine("");

                        protocolCounter += 1000;

                        fileContentsBuilderJavascript.AppendLine("    Modify" + tablename + "(existing" + tablename + ": " + tablename + ", transactionId: string, cb)");
                        fileContentsBuilderJavascript.AppendLine("    {");
                        fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eUpdate" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.transactionid = transactionId;");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = existing" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                        fileContentsBuilderJavascript.AppendLine("    }");
                        fileContentsBuilderJavascript.AppendLine("");

                        protocolCounter += 1000;

                        fileContentsBuilderJavascript.AppendLine("    Delete" + tablename + "(existing" + tablename + ": " + tablename + ", transactionId: string, cb)");
                        fileContentsBuilderJavascript.AppendLine("    {");
                        fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eDelete" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.transactionid = transactionId;");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = existing" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                        fileContentsBuilderJavascript.AppendLine("    }");
                        fileContentsBuilderJavascript.AppendLine("");

                        protocolCounter += 1000;

                        fileContentsBuilderJavascript.AppendLine("    RetrieveByID" + tablename + "(existing" + tablename + "id: Number, transactionId: string, cb)");
                        fileContentsBuilderJavascript.AppendLine("    {");
                        fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eRetrieveByID" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.transactionid = transactionId;");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = existing" + tablename + "id;");
                        fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                        fileContentsBuilderJavascript.AppendLine("    }");
                        fileContentsBuilderJavascript.AppendLine("");

                        protocolCounter += 1000;

                        fileContentsBuilderJavascript.AppendLine("    RetrieveWithWhereClause" + tablename + "(WhereClause: string, transactionId: string, cb)");
                        fileContentsBuilderJavascript.AppendLine("    {");
                        fileContentsBuilderJavascript.AppendLine("        var objectToSend = new ProtocolPacket();");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.protocol = eRetrieveWithWhereClause" + tablename + ";");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.transactionid = transactionId;");
                        fileContentsBuilderJavascript.AppendLine("        objectToSend.payload = WhereClause;");
                        fileContentsBuilderJavascript.AppendLine("        this.postAjaj(objectToSend, cb);");
                        fileContentsBuilderJavascript.AppendLine("    }");
                        fileContentsBuilderJavascript.AppendLine("");

                        protocolCounter += 1000;
                    }

                    fileContentsBuilderJavascript.AppendLine("}");

                    File.WriteAllText(javascriptpath + Path.DirectorySeparatorChar + fileprefix + "ajajclient.ts", fileContentsBuilderJavascript.ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }
    }
}
