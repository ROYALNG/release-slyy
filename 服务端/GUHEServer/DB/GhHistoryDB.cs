
using GHCore;
using GHIBMS.Common;
using GHIBMS.Interface;
using GHIBMS.Server.Models;
using System;
using System.Data;
using System.Data.Common;

namespace GHIBMS.Server
{
    public class GhHistroyDB
    {
        #region Instance
        private static GhHistroyDB _instance;
        GHCore.Database.IGHDatabaseHelper sqlhelper;
        public GhHistroyDB(string connstring)
        {
            string providerType = "GHDatabase.Mysql.MysqlHelper,GHDatabase.Mysql";
            //string  connstring = System.Configuration.ConfigurationManager.AppSettings["ghHistoryDb"];
            sqlhelper = GHCore.Database.GHDatabaseFactory.CreateDatabase(providerType, connstring);
        }
        public static GhHistroyDB GetInstance(string ip, string port, string name, string user, string password)
        {
            string connstring = $"server={ip};user={user};database={name};port={port};password={password};";
            if (_instance == null)
                _instance = new GhHistroyDB(connstring);

            return _instance;
        }
        public bool TestConn()
        {
            try
            {
                DbConnection conn = sqlhelper.Connection;
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                  
                    conn.Close();
                    return true;

                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
            return false;

        }
        #endregion

        #region GHSTATISTICS_Data_yyyyMM
        private string Get_tableName
        {
            get
            {
                return "GH_History" + DateTime.Now.ToString("yyyyMMdd");
            }
        }
        private bool create_his_tableName(string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
                tableName = Get_tableName;
            string sql = string.Format(@" CREATE TABLE IF NOT EXISTS `{0}` (
	             `ID` int NOT NULL AUTO_INCREMENT,
                  `time` datetime  NULL DEFAULT NULL,
                  `server` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                  `channel` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                  `controller` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                  `varKey` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                  `variable` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                  `value` double NULL DEFAULT NULL,
                  PRIMARY KEY (`ID`) USING BTREE
            )
            COLLATE='utf8_general_ci'
            ENGINE=InnoDB;", tableName);
            try
            {
                return sqlhelper.ExecuteNonQuery(sqlhelper.ConnectionString, sql) > 0;
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Exception);
            }
            return false;

        }
        private bool checkTableExists(string tableName)
        {
            string sql = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE  table_schema = '{0}' and table_name = '{1}';",ServerConfig.DbName, tableName);
            try
            {
                object obj = sqlhelper.ExecuteScalar(sqlhelper.ConnectionString, sql, null);
                if (obj != System.DBNull.Value && obj != null)
                {
                    return int.Parse(obj.ToString()) > 0;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Exception);
            }
            return false;
        }
        public void CreateHistTable()
        {
           
            string  tableName = Get_tableName;
            if (!checkTableExists(tableName))
                create_his_tableName(tableName);
        }

        public int AddRecord(string key, IVariable var, string tableName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName))
                    tableName = Get_tableName;
                //if (!checkTableExists(tableName)) 
                //   if  (!create_his_tableName(tableName))
                //    return -1;

                string sql = "insert into {0} (time,server,channel,controller,varKey,value,variable) values ( @time,@server,@channel,@controller,@varKey,@value,@variable ) ;";
                sql = string.Format(sql, tableName);
                return sqlhelper.ExecuteNonQuery(sqlhelper.ConnectionString, sql, DateTime.Now, ServerConfig.CloudClientID, var.ControllerObject.ChannelObject.ID, var.ControllerObject.ID, var.ID, pubFun.IsDouble(var.Value.ToString(), 0), key);
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Exception);
            }
            return -1;
        }
        public int AddRecord(string key, History his, string tableName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName))
                    tableName = Get_tableName;
                //if (!checkTableExists(tableName))
                //    if (!create_his_tableName(tableName))
                //        return -1;

                string sql = "insert into {0} (time,server,channel,controller,varKey,value,variable) values ( @time,@server,@channel,@controller,@varKey,@value,@variable ) ;";
                sql = string.Format(sql, tableName);
                return sqlhelper.ExecuteNonQuery(sqlhelper.ConnectionString, sql,DateTime.Now, ServerConfig.CloudClientID, his.channel, his.controller,his.varKey, pubFun.IsDouble(his.value.ToString(), 0), key);
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Exception);
            }
            return -1;
        }
        #endregion


    }
}
