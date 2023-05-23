using GH.RTDB.Server.Models;
using GHCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.Common;

namespace GHIBMS.Server
{
    public class Database
    {
        #region Instance
        private static Database _instance ;
        GHCore.Database.IGHDatabaseHelper sqlhelper;
        private Database(string connstring)
        {
            sqlhelper = GHCore.Database.GHDatabaseFactory.CreateDatabase();
        }
        public static Database GetInstance(string ip, string name, string user, string password)
        {
            string connstring = $"server={ip};user={user};database={name};port=32769;password={password};";
            if (_instance == null)
                _instance = new Database(connstring) ;

            return _instance;
        }
        public  bool TestConn()
        {
           
            try
            {
                DbConnection conn =sqlhelper.Connection;
                return  true ;
            }catch
            {
                return false;
            }

        }
        #endregion

 
    }
}