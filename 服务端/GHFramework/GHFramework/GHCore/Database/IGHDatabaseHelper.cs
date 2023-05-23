using System.Data;
using System.Data.Common;
using System.Xml;

namespace GHCore.Database
{
    public interface IGHDatabaseHelper
    {
        string Name { get; }

        string ConnectionString { get; set; }

        DbConnection Connection { get; }

        DbCommand CreateCommand(DbConnection connection, string commandText, params DbParameter[] commandParameters);

        DbCommand CreateCommand(string connectionString, string commandText, params DbParameter[] commandParameters);

        DbParameter CreateParameter(string parameterName, System.Data.DbType parameterType, object parameterValue);

        DataSet ExecuteDataSet(string connectionString, string commandText, object[] paramList);

        DataSet ExecuteDataSet(DbConnection cn, string commandText, object[] paramList);

        DataSet ExecuteDataset(DbCommand cmd);

        DataSet ExecuteDataset(DbTransaction transaction, string commandText, params DbParameter[] commandParameters);

        DataSet ExecuteDataset(DbTransaction transaction, string commandText, object[] commandParameters);

        void UpdateDataset(DbCommand insertCommand, DbCommand deleteCommand, DbCommand updateCommand, DataSet dataSet, string tableName);

        IDataReader ExecuteReader(DbCommand cmd, string commandText, object[] paramList);

        int ExecuteNonQuery(string connectionString, string commandText, params object[] paramList);

        int ExecuteNonQuery(DbConnection cn, string commandText, params object[] paramList);

        int ExecuteNonQuery(DbTransaction transaction, string commandText, params object[] paramList);

        int ExecuteNonQuery(IDbCommand cmd);

        object ExecuteScalar(string connectionString, string commandText, params object[] paramList);

        XmlReader ExecuteXmlReader(IDbCommand command);

        int ExecuteNonQueryTypedParams(IDbCommand command, DataRow dataRow);

    }
}
