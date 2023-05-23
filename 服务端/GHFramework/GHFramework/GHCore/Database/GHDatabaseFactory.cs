using System;

namespace GHCore.Database
{
    public class GHDatabaseFactory
    {
        public static IGHDatabaseHelper CreateDatabase(string providerType = null, string connstring = null)
        {
            try
            {
                if (string.IsNullOrEmpty(providerType))
                    providerType = "GHDatabase.Mysql.MysqlHelper,GHDatabase.Mysql";
                if (string.IsNullOrEmpty(connstring))
                    connstring = System.Configuration.ConfigurationManager.AppSettings["ghDefaultDb"];

                Type type = Type.GetType(providerType);
                IGHDatabaseHelper dbhelper = (IGHDatabaseHelper)Activator.CreateInstance(type);
                if (!string.IsNullOrEmpty(connstring))
                {
                    dbhelper.ConnectionString = connstring;
                }
                return dbhelper;
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.ToString(), LogCategory.Exception);
            }

            return null;
        }

        public static GHDBProviderType GetDbProviderType(string typeName)
        {
            switch (typeName)
            {
                case "MsSql":
                    return GHDBProviderType.MsSql;
                case "MySql":
                    return GHDBProviderType.MySql;
                case "Oracle":
                    return GHDBProviderType.Oracle;
                case "Sqlite":
                    return GHDBProviderType.Sqlite;
                default:
                    return GHDBProviderType.MsSql;
            }
        }
    }

    public enum GHDBProviderType
    {
        /// <summary>
        /// MS SQL
        /// </summary>
        MsSql,
        /// <summary>
        /// MySql
        /// </summary>
        MySql,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// Sqlite
        /// </summary>
        Sqlite
    }
}
