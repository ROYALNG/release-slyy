﻿using GHCore.Database;
using MySql.Data.MySqlClient;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace GHDatabase.Mysql
{
    public class MysqlHelper : IGHDatabaseHelper
    {
        //private static readonly MysqlHelper _instance = new MysqlHelper();

        /// <summary>
        /// Creates a new <see cref="MySqlHelper"/> instance. The ctor is marked private since all members are static.
        /// </summary>
        public MysqlHelper() { }
        //单实例
        //public static MysqlHelper SingletonInstance
        //{
        //    get 
        //    {
        //        return _instance;
        //    }
        //}

        public string Name
        {
            get
            {
                return "MySql";
            }
        }

        string _connstring = "";
        public string ConnectionString
        {
            get
            {
                //string connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false",
				//server.Text, userid.Text, password.Text );
                //myConnectionString = "Database=Test;Data Source=localhost;User Id=username;Password=pass";
                if (string.IsNullOrEmpty(_connstring))
                    return System.Configuration.ConfigurationManager.AppSettings["ghDefaultDb"];
                else
                    return _connstring;
            }
            set
            {
                _connstring = value;
            }
        }

        MySqlConnection _connection;
        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    lock (_connection)
                    {
                        if (_connection == null)
                        {
                            _connection = new MySqlConnection(ConnectionString);
                            return _connection;
                        }
                    }
                }
                return _connection;
            }
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="connection">Connection.</param>
        /// <param name="commandText">Command text.</param>
        /// <param name="commandParameters">Command parameters.</param>
        /// <returns>SQLite Command</returns>
        public DbCommand CreateCommand(DbConnection connection, string commandText, params DbParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand(commandText, connection as MySqlConnection);
            if (commandParameters.Length > 0)
            {
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
            return cmd;
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="commandText">Command text.</param>
        /// <param name="commandParameters">Command parameters.</param>
        /// <returns>MySql Command</returns>
        public DbCommand CreateCommand(string connectionString, string commandText, params DbParameter[] commandParameters)
        {
            MySqlConnection cn = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand(commandText, cn);

            if (commandParameters.Length > 0)
            {
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
            return cmd;
        }
        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterType">Parameter type.</param>
        /// <param name="parameterValue">Parameter value.</param>
        /// <returns>MySqlParameter</returns>
        public DbParameter CreateParameter(string parameterName, System.Data.DbType parameterType, object parameterValue)
        {
            MySqlParameter parameter = new MySqlParameter();
            parameter.DbType = parameterType;
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            return parameter;
        }

        /// <summary>
        /// Shortcut method to execute dataset from SQL Statement and object[] arrray of parameter values
        /// </summary>
        /// <param name="connectionString">MySql Connection string</param>
        /// <param name="commandText">SQL Statement with embedded "@param" style parameter names</param>
        /// <param name="paramList">object[] array of parameter values</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string connectionString, string commandText, object[] paramList)
        {
            MySqlConnection cn = new MySqlConnection(connectionString);
            MySqlCommand cmd = cn.CreateCommand();


            cmd.CommandText = commandText;
            if (paramList != null)
            {
                AttachParameters(cmd, commandText, paramList);
            }
            DataSet ds = new DataSet();
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            cn.Close();
            return ds;
        }
        /// <summary>
        /// Shortcut method to execute dataset from SQL Statement and object[] arrray of  parameter values
        /// </summary>
        /// <param name="cn">Connection.</param>
        /// <param name="commandText">Command text.</param>
        /// <param name="paramList">Param list.</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(DbConnection cn, string commandText, object[] paramList)
        {

            MySqlCommand cmd = (cn as MySqlConnection).CreateCommand();


            cmd.CommandText = commandText;
            if (paramList != null)
            {
                AttachParameters(cmd, commandText, paramList);
            }
            DataSet ds = new DataSet();
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            cn.Close();
            return ds;
        }
        /// <summary>
        /// Executes the dataset from a populated Command object.
        /// </summary>
        /// <param name="cmd">Fully populated MySqlCommand</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataset(DbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Closed)
                cmd.Connection.Open();
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            da.Fill(ds);
            da.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            return ds;
        }

        /// <summary>
        /// Executes the dataset in a MySql Transaction
        /// </summary>
        /// <param name="transaction">MySqlTransaction. Transaction consists of Connection, Transaction,  /// and Command, all of which must be created prior to making this method call. </param>
        /// <param name="commandText">Command text.</param>
        /// <param name="commandParameters">MySql Command parameters.</param>
        /// <returns>DataSet</returns>
        /// <remarks>user must examine Transaction Object and handle transaction.connection .Close, etc.</remarks>
        public DataSet ExecuteDataset(DbTransaction transaction, string commandText, params DbParameter[] commandParameters)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or committed, please provide an open transaction.", "transaction");
            IDbCommand cmd = transaction.Connection.CreateCommand();
            cmd.CommandText = commandText;
            foreach (MySqlParameter parm in commandParameters)
            {
                cmd.Parameters.Add(parm);
            }
            if (transaction.Connection.State == ConnectionState.Closed)
                transaction.Connection.Open();
            DataSet ds = ExecuteDataset((MySqlCommand)cmd);
            return ds;
        }

        /// <summary>
        /// Executes the dataset with Transaction and object array of parameter values.
        /// </summary>
        /// <param name="transaction">MySqlTransaction. Transaction consists of Connection, Transaction,    /// and Command, all of which must be created prior to making this method call. </param>
        /// <param name="commandText">Command text.</param>
        /// <param name="commandParameters">object[] array of parameter values.</param>
        /// <returns>DataSet</returns>
        /// <remarks>user must examine Transaction Object and handle transaction.connection .Close, etc.</remarks>
        public DataSet ExecuteDataset(DbTransaction transaction, string commandText, object[] commandParameters)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or committed,                                                          please provide an open transaction.", "transaction");
            IDbCommand cmd = transaction.Connection.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters((MySqlCommand)cmd, cmd.CommandText, commandParameters);
            if (transaction.Connection.State == ConnectionState.Closed)
                transaction.Connection.Open();

            DataSet ds = ExecuteDataset((MySqlCommand)cmd);
            return ds;
        }

        #region UpdateDataset
        /// <summary>
        /// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        /// </remarks>
        /// <param name="insertCommand">A valid SQL statement  to insert new records into the data source</param>
        /// <param name="deleteCommand">A valid SQL statement to delete records from the data source</param>
        /// <param name="updateCommand">A valid SQL statement used to update records in the data source</param>
        /// <param name="dataSet">The DataSet used to update the data source</param>
        /// <param name="tableName">The DataTable used to update the data source.</param>
        public void UpdateDataset(DbCommand insertCommand, DbCommand deleteCommand, DbCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null) throw new ArgumentNullException("insertCommand");
            if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
            if (updateCommand == null) throw new ArgumentNullException("updateCommand");
            if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

            // Create a MySqlDataAdapter, and dispose of it after we are done
            using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter())
            {
                // Set the data adapter commands
                dataAdapter.UpdateCommand = updateCommand as MySqlCommand;
                dataAdapter.InsertCommand = insertCommand as MySqlCommand;
                dataAdapter.DeleteCommand = deleteCommand as MySqlCommand;

                // Update the dataset changes in the data source
                dataAdapter.Update(dataSet, tableName);

                // Commit all the changes made to the DataSet
                dataSet.AcceptChanges();
            }
        }
        #endregion




        /// <summary>
        /// ShortCut method to return IDataReader
        /// NOTE: You should explicitly close the Command.connection you passed in as
        /// well as call Dispose on the Command  after reader is closed.
        /// We do this because IDataReader has no underlying Connection Property.
        /// </summary>
        /// <param name="cmd">MySqlCommand Object</param>
        /// <param name="commandText">SQL Statement with optional embedded "@param" style parameters</param>
        /// <param name="paramList">object[] array of parameter values</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExecuteReader(DbCommand cmd, string commandText, object[] paramList)
        {
            if (cmd.Connection == null)
                throw new ArgumentException("Command must have live connection attached.", "cmd");
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cmd.Connection.State == ConnectionState.Closed)
                cmd.Connection.Open();
            IDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }

        /// <summary>
        /// Shortcut to ExecuteNonQuery with SqlStatement and object[] param values
        /// </summary>
        /// <param name="connectionString">MySql Connection String</param>
        /// <param name="commandText">Sql Statement with embedded "@param" style parameters</param>
        /// <param name="paramList">object[] array of parameter values</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string connectionString, string commandText, params object[] paramList)
        {
            MySqlConnection cn = new MySqlConnection(connectionString);
            MySqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            cn.Close();

            return result;
        }



        public int ExecuteNonQuery(DbConnection cn, string commandText, params  object[] paramList)
        {

            MySqlCommand cmd = (cn as MySqlConnection).CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            cn.Close();

            return result;
        }

        /// <summary>
        /// Executes  non-query sql Statment with Transaction
        /// </summary>
        /// <param name="transaction">MySqlTransaction. Transaction consists of Connection, Transaction,   /// and Command, all of which must be created prior to making this method call. </param>
        /// <param name="commandText">Command text.</param>
        /// <param name="paramList">Param list.</param>
        /// <returns>Integer</returns>
        /// <remarks>user must examine Transaction Object and handle transaction.connection .Close, etc.</remarks>
        public int ExecuteNonQuery(DbTransaction transaction, string commandText, params  object[] paramList)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or committed,                                                        please provide an open transaction.", "transaction");
            IDbCommand cmd = transaction.Connection.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters((MySqlCommand)cmd, cmd.CommandText, paramList);
            if (transaction.Connection.State == ConnectionState.Closed)
                transaction.Connection.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return result;
        }


        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="cmd">CMD.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Closed)
                cmd.Connection.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cmd.Dispose();
            return result;
        }

        /// <summary>
        /// Shortcut to ExecuteScalar with Sql Statement embedded params and object[] param values
        /// </summary>
        /// <param name="connectionString">MySql Connection String</param>
        /// <param name="commandText">SQL statment with embedded "@param" style parameters</param>
        /// <param name="paramList">object[] array of param values</param>
        /// <returns></returns>
        public object ExecuteScalar(string connectionString, string commandText, params  object[] paramList)
        {
            MySqlConnection cn = new MySqlConnection(connectionString);
            MySqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            object result = cmd.ExecuteScalar();
            cmd.Dispose();
            cn.Close();

            return result;
        }

        /// <summary>
        /// Execute XmlReader with complete Command
        /// </summary>
        /// <param name="command">MySql Command</param>
        /// <returns>XmlReader</returns>
        public XmlReader ExecuteXmlReader(IDbCommand command)
        { // open the connection if necessary, but make sure we 
            // know to close it when we�re done.
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            // get a data adapter  
            MySqlDataAdapter da = new MySqlDataAdapter((MySqlCommand)command);
            DataSet ds = new DataSet();
            // fill the data set, and return the schema information
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds);
            // convert our dataset to XML
            StringReader stream = new StringReader(ds.GetXml());
            command.Connection.Close();
            // convert our stream of text to an XmlReader
            return new XmlTextReader(stream);
        }



        /// <summary>
        /// Parses parameter names from SQL Statement, assigns values from object array ,   
        /// and returns fully populated ParameterCollection.
        /// </summary>
        /// <param name="commandText">Sql Statement with "@param" style embedded parameters</param>
        /// <param name="paramList">object[] array of parameter values</param>
        /// <returns>MySqlParameterCollection</returns>
        /// <remarks>Status experimental. Regex appears to be handling most issues. Note that parameter object array must be in same 
        ///order as parameter names appear in SQL statement.
        ///</remarks>
        private DbParameterCollection AttachParameters(DbCommand cmd, string commandText, params  object[] paramList)
        {
            if (paramList == null || paramList.Length == 0) return null;

            MySqlParameterCollection coll = (cmd as MySqlCommand).Parameters;
            string parmString = commandText.Substring(commandText.IndexOf("@"));
            // pre-process the string so always at least 1 space after a comma.
            parmString = parmString.Replace(",", " ,");
            // get the named parameters into a match collection
            string pattern = @"(@)\S*(.*?)\b";
            Regex ex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection mc = ex.Matches(parmString);
            string[] paramNames = new string[mc.Count];
            int i = 0;
            foreach (Match m in mc)
            {
                paramNames[i] = m.Value;
                i++;
            }

            // now let's type the parameters
            int j = 0;
            Type t = null;
            foreach (object o in paramList)
            {
                if (o == null)
                {
                    MySqlParameter nullparm = new MySqlParameter();
                    nullparm.ParameterName = paramNames[j];
                    nullparm.IsNullable = true;
                    coll.Add(nullparm);
                    j++;
                    continue;
                }
                t = o.GetType();

                MySqlParameter parm = new MySqlParameter();
                switch (t.ToString())
                {

                    case ("DBNull"):
                    case ("Char"):
                    case ("SByte"):
                    case ("UInt16"):
                    case ("UInt32"):
                    case ("UInt64"):
                        throw new SystemException("Invalid data type");


                    case ("System.String"):
                        parm.DbType = DbType.String;//MySqlDbType.String
                        parm.ParameterName = paramNames[j];
                        parm.Value = (string)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Byte[]"):
                        parm.DbType = DbType.Binary; //MySqlDbType.Binary
                        parm.ParameterName = paramNames[j];
                        parm.Value = (byte[])paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Int32"):
                        parm.DbType = DbType.Int32; //MySqlDbType.Int32
                        parm.ParameterName = paramNames[j];
                        parm.Value = (int)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Boolean"):
                        parm.DbType = DbType.Boolean; //MySqlDbType.Bit
                        parm.ParameterName = paramNames[j];
                        parm.Value = (bool)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.DateTime"):
                        parm.DbType = DbType.DateTime; //MySqlDbType.DateTime
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDateTime(paramList[j]);
                        coll.Add(parm);
                        break;

                    case ("System.Double"):
                        parm.DbType = DbType.Double; //MySqlDbType.Double
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDouble(paramList[j]);
                        coll.Add(parm);
                        break;

                    case ("System.Decimal"):
                        parm.DbType = DbType.Decimal; //MySqlDbType.Decimal
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDecimal(paramList[j]);
                        break;

                    case ("System.Guid"):
                        parm.DbType = DbType.Guid;  //MySqlDbType.Guid
                        parm.ParameterName = paramNames[j];
                        parm.Value = (System.Guid)(paramList[j]);
                        break;

                    case ("System.Object"):

                        parm.DbType = DbType.Object; //MySqlDbType.Blob
                        parm.ParameterName = paramNames[j];
                        parm.Value = paramList[j];
                        coll.Add(parm);
                        break;

                    default:
                        throw new SystemException("Value is of unknown data type");

                } // end switch

                j++;
            }
            return coll;
        }

        /// <summary>
        /// Executes non query typed params from a DataRow
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="dataRow">Data row.</param>
        /// <returns>Integer result code</returns>
        public int ExecuteNonQueryTypedParams(IDbCommand command, DataRow dataRow)
        {
            int retVal = 0;

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Set the parameters values
                AssignParameterValues(command.Parameters, dataRow);

                retVal = ExecuteNonQuery(command);
            }
            else
            {
                retVal = ExecuteNonQuery(command);
            }

            return retVal;
        }

        /// <summary>
        /// This method assigns dataRow column values to an IDataParameterCollection
        /// </summary>
        /// <param name="commandParameters">The IDataParameterCollection to be assigned values</param>
        /// <param name="dataRow">The dataRow used to hold the command's parameter values</param>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the parameter names are invalid.</exception>
        protected internal void AssignParameterValues(IDataParameterCollection commandParameters, DataRow dataRow)
        {
            if (commandParameters == null || dataRow == null)
            {
                // Do nothing if we get no data
                return;
            }

            DataColumnCollection columns = dataRow.Table.Columns;

            int i = 0;
            // Set the parameters values
            foreach (IDataParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null ||
                 commandParameter.ParameterName.Length <= 1)
                    throw new InvalidOperationException(string.Format(
                           "Please provide a valid parameter name on the parameter #{0},                            the ParameterName property has the following value: '{1}'.",
                     i, commandParameter.ParameterName));

                if (columns.Contains(commandParameter.ParameterName))
                    commandParameter.Value = dataRow[commandParameter.ParameterName];
                else if (columns.Contains(commandParameter.ParameterName.Substring(1)))
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];

                i++;
            }
        }

        /// <summary>
        /// This method assigns dataRow column values to an array of IDataParameters
        /// </summary>
        /// <param name="commandParameters">Array of IDataParameters to be assigned values</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the parameter names are invalid.</exception>
        protected void AssignParameterValues(IDataParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                // Do nothing if we get no data
                return;
            }

            DataColumnCollection columns = dataRow.Table.Columns;

            int i = 0;
            // Set the parameters values
            foreach (IDataParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null ||
                 commandParameter.ParameterName.Length <= 1)
                    throw new InvalidOperationException(string.Format(
                     "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                     i, commandParameter.ParameterName));

                if (columns.Contains(commandParameter.ParameterName))
                    commandParameter.Value = dataRow[commandParameter.ParameterName];
                else if (columns.Contains(commandParameter.ParameterName.Substring(1)))
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];

                i++;
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of IDataParameters
        /// </summary>
        /// <param name="commandParameters">Array of IDataParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        /// <exception cref="System.ArgumentException">Thrown if an incorrect number of parameters are passed.</exception>
        protected void AssignParameterValues(IDataParameter[] commandParameters, params  object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the IDataParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length, k = 0; i < j; i++)
            {
                if (commandParameters[i].Direction != ParameterDirection.ReturnValue)
                {
                    // If the current array value derives from IDataParameter, then assign its Value property
                    if (parameterValues[k] is IDataParameter)
                    {
                        IDataParameter paramInstance;
                        paramInstance = (IDataParameter)parameterValues[k];
                        if (paramInstance.Direction == ParameterDirection.ReturnValue)
                        {
                            paramInstance = (IDataParameter)parameterValues[++k];
                        }
                        if (paramInstance.Value == null)
                        {
                            commandParameters[i].Value = DBNull.Value;
                        }
                        else
                        {
                            commandParameters[i].Value = paramInstance.Value;
                        }
                    }
                    else if (parameterValues[k] == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = parameterValues[k];
                    }
                    k++;
                }
            }
        }
    }
}
