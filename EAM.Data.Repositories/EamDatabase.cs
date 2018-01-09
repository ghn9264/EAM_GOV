using System;
using System.Data;
using NPoco;

namespace EAM.Data.Repositories
{
    public class EamDatabase : Database
    {
         private static string ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["AssetsDb"].ConnectionString;
       
        // @"Server=localhost;Database=assetsdb;User=root;Password=admin@888;pooling=true;charset=utf8";
         
        public EamDatabase()
            : base(ConnStr, DatabaseType.MySQL)
        {
        }
        protected override void OnExecutingCommand(IDbCommand cmd)
        {
            base.OnExecutingCommand(cmd);
            Console.WriteLine(FormatCommand(cmd));
        }
    }
}