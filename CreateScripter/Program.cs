using System;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Common;
using Microsoft.IdentityModel.Tokens;

namespace CreateScripter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string server = GetArgumentValue(args, "server");
            string db = GetArgumentValue(args, "db");
            string user = GetArgumentValue(args, "user");
            string pass = GetArgumentValue(args, "pass");
            string obj = GetArgumentValue(args, "obj");

           
            if (server.IsNullOrEmpty())
            {
                Console.WriteLine("インスタンスを指定してください。");
                return;
            }
            if (db.IsNullOrEmpty())
            {
                Console.WriteLine("データベースを指定してください。");
                return;
            }
            if (user.IsNullOrEmpty())
            {
                Console.WriteLine("ユーザーを指定してください。");
                return;
            }
            if (pass.IsNullOrEmpty())
            {
                Console.WriteLine("パスワードを指定してください。");
                return;
            }
            if (obj.IsNullOrEmpty())
            {
                Console.WriteLine("DBオブジェクトを指定してください。");
                return;
            }

            try
            {
                WriteTextToFile(CreateScript(server, db, user, pass, obj), obj + ".sql");
                Console.WriteLine("出力が完了しました。");
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラーが発生しました: " + ex.Message);
            }

        }

        static void WriteTextToFile(string text, string filePath)
        {

            if (text.IsNullOrEmpty())
            {
                return;
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(text);
            }
        }
        static string CreateScript(string serverInstance, string dbName, string login, string password, string obj)
        {
     
            ServerConnection srvConn = new ServerConnection();
            srvConn.ServerInstance = serverInstance;   // connects to named instance  
            srvConn.LoginSecure = false;   // set to true for Windows Authentication  
            srvConn.Login = login;
            srvConn.Password = password;
            Server srv = new Server(srvConn);

            // Reference the database.    
            Database db = srv.Databases[dbName];

            // Define a Scripter object and set the required scripting options.   
            Scripter scrp = new Scripter(srv);
            scrp.Options.ScriptDrops = false;
            scrp.Options.Indexes = true; // インデックスを含める  
            scrp.Options.ClusteredIndexes = true; // クラスター化インデックスを含める
            scrp.Options.Triggers = true; //トリガーを含める
            scrp.Options.DriAll = true; //参照整合性の出力を含める

            StringBuilder sb = new StringBuilder();

           foreach (Table dbObj in db.Tables)
            {
                if (dbObj.Name == obj)
                {
                    // Generating script for table tb  
                    System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { dbObj.Urn });
                    foreach (string st in sc)
                    {
                        sb.Append(st + "\r\n" + "GO" + "\r\n" + "\r\n");
                    }

                }
            }
            foreach (View dbObj in db.Views)
            {
                if (dbObj.Name == obj)
                {
                    // Generating script for table tb  
                    System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { dbObj.Urn });
                    foreach (string st in sc)
                    {
                        sb.Append(st + "\r\n" + "GO" + "\r\n" + "\r\n");
                    }

                }
            }
            foreach (StoredProcedure dbObj in db.StoredProcedures)
            {
                if (dbObj.Name == obj)
                {
                    // Generating script for table tb  
                    System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { dbObj.Urn });
                    foreach (string st in sc)
                    {
                        sb.Append(st + "\r\n" + "GO" + "\r\n" + "\r\n");
                    }

                }
            }
            foreach (UserDefinedFunction dbObj in db.UserDefinedFunctions)
            {
                if (dbObj.Name == obj)
                {
                    // Generating script for table tb  
                    System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { dbObj.Urn });
                    foreach (string st in sc)
                    {
                        sb.Append(st + "\r\n" + "GO" + "\r\n" + "\r\n");
                    }

                }
            }
            foreach (UserDefinedTableType dbObj in db.UserDefinedTableTypes)
            {
                if (dbObj.Name == obj)
                {
                    // Generating script for table tb  
                    System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { dbObj.Urn });
                    foreach (string st in sc)
                    {
                        sb.Append(st + "\r\n" + "GO" + "\r\n" + "\r\n");
                    }

                }
            }
            foreach (Synonym dbObj in db.Synonyms)
            {
                if (dbObj.Name == obj)
                {
                    // Generating script for table tb  
                    System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { dbObj.Urn });
                    foreach (string st in sc)
                    {
                        sb.Append(st + "\r\n" + "GO" + "\r\n" + "\r\n");
                    }

                }
            }
            return sb.ToString();
        }

        public static string GetArgumentValue(string[] args, string argumentName)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-" + argumentName && i + 1 < args.Length)
                {
                    return args[i + 1];
                }
            }
            return null;
        }
    }
}
