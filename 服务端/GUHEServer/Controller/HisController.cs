using GHDatabase.Influxdb;
using GHIBMS.Common;
using GHIBMS.Interface;
using GHIBMS.Server.Models;
using InfluxData.Net.InfluxDb.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace GHIBMS.Server
{
    public class RecordController
    {
        private int SLEEP_TIME = 1000;
        private bool active = false;
        private Thread thread;

        public delegate void onErrorDelegate(Exception e);
        public event onErrorDelegate onError;
        private static ConcurrentQueue<History> varChangedRecord = new ConcurrentQueue<History>();
        //等待变化上传历史记录的最大数量
        private static int MaxChangeRecord = 10000;

        /// <summary>
        /// 是否已启动
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {

            thread = new Thread(new ThreadStart(WriteHistory));
            //线程名，调试用
            thread.Name = "History_thread";
            thread.IsBackground = true;
            active = true;
            thread.Start();
        }
        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {
            active = false;
            for (int i = 0; i < 50; i++)
            {

                if (thread != null)
                {
                    Thread.Sleep(100);
                }
                else
                    break;

            }
            try
            {
                if (thread != null)
                {
                    thread.Interrupt();
                }
            }
            catch { }
        }

        //内部线程
        private void WriteHistory()
        {
            try
            {
                DateTime dtFaster = DateTime.Now;
                DateTime dtNormal = DateTime.Now;
                DateTime dtSlow =   DateTime.Now;
                dtFaster= dtFaster.AddSeconds(ServerConfig.TimeFastRecorder);
                dtNormal= dtNormal.AddSeconds(ServerConfig.TimeNormalRecorder * 60);
                dtSlow=dtSlow.AddSeconds(ServerConfig.TimeSlowRecorder * 60 * 60);
                GhHistroyDB mysqldb = null;
                 InfluxdbHelper influxdb = null;
                if (ServerConfig.DataBaseEnable)
                {
                    mysqldb = GhHistroyDB.GetInstance(ServerConfig.DbHost, ServerConfig.DBPort.ToString(), ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                }
                if (ServerConfig.influxEnalble)
                {
                    influxdb = InfluxdbHelper.GetInstance(ServerConfig.influxIP + ":" + ServerConfig.influxPort, ServerConfig.influxUsername, ServerConfig.influxPassword);
                }
                while (active)
                {
                    try
                    {

                        if(mysqldb!=null)
                            mysqldb.CreateHistTable();
                        TimeSpan starttime = new TimeSpan(DateTime.Now.Ticks);
             
                        
                        if(ServerConfig.EnableFastRecorder && dtFaster<DateTime.Now)
                        {
                            foreach (IChannel chan in Rtdb.ChanList)
                                foreach (IController con in chan.ConList)
                                    foreach (var item in con.VarList)
                                    {
                                        if (item.HistoryRecorder == HistoryTimerRecordEnum.FAST)
                                        {
                                            VariableChangedRecord(GetHistory(item));
                                            //断线记录
                                            if (ServerConfig.EnableMqttHisRecord)
                                            {
                                                if (!MqttController.IsConnected())
                                                    sentToMqtt(chan, con, item);
                                            }
                                        }
                                    }

                            dtFaster= dtFaster.AddSeconds(ServerConfig.TimeFastRecorder);
                        }
                        if (ServerConfig.EnableNormalRecorder && dtNormal < DateTime.Now)
                        {
                            foreach (IChannel chan in Rtdb.ChanList)
                                foreach (IController con in chan.ConList)
                                    foreach (var item in con.VarList)
                                    {
                                        if (item.HistoryRecorder == HistoryTimerRecordEnum.STANDARD)
                                        {
                                            VariableChangedRecord(GetHistory(item));
                                            //断线记录
                                            if (ServerConfig.EnableMqttHisRecord)
                                            {
                                                if (!MqttController.IsConnected())
                                                    sentToMqtt(chan, con, item);
                                            }
                                        }
                                    }

                            dtNormal= dtNormal.AddSeconds(ServerConfig.TimeNormalRecorder*60);
                        }
                       
                        if (ServerConfig.EnableSlowRecorder && dtSlow < DateTime.Now)
                        {
                            foreach (IChannel chan in Rtdb.ChanList)
                                foreach (IController con in chan.ConList)
                                    foreach (var item in con.VarList)
                                    {
                                        if (item.HistoryRecorder == HistoryTimerRecordEnum.SLOW)
                                        {
                                            VariableChangedRecord(GetHistory(item));
                                            //断线记录
                                            if (ServerConfig.EnableMqttHisRecord)
                                            {
                                                if (!MqttController.IsConnected())
                                                    sentToMqtt(chan, con, item);
                                            }
                                        }
                                    }

                            dtSlow= dtSlow.AddSeconds(ServerConfig.TimeSlowRecorder * 60*60);
                        }
                      //处理变化记录
                        try
                        {
                            int mCount = varChangedRecord.Count;
                            if (mCount > 0)
                            {
                                List<string> list = new List<string>();
                                List<Point> points = new List<Point>();
                                for (int i = 1; i <= mCount; i++)
                                {
                                    History his;
                                    varChangedRecord.TryDequeue(out his);
                                    if (ServerConfig.DataBaseEnable)
                                    {
                                        //mysql
                                        string key = $"{ his.server}:{his.channel}:{his.controller }:{his.controller}:{his.varKey}";
                                        string sql = $"insert into {Get_tableName()} (time,server,channel,controller,varKey,value,variable) values ( '{his.time}','{his.server}','{his.channel}','{his.controller}','{his.varKey}','{his.value}','{key}' ) ;";
                                        list.Add(sql);
                                    }
                                    if(ServerConfig.influxEnalble)
                                    {
                                        Point point = new Point
                                        {
                                            Name = "gh_history", // serie/measurement/table to write into
                                            Tags = new Dictionary<string, object>()
                                            {
                                               { "variable",ServerConfig.CloudClientID+":"+his.channel+":"+his.controller+":"+his.varKey },
                                            },
                                             Fields = new Dictionary<string, object>()
                                            {
                                                { "server",ServerConfig.CloudClientID},
                                                { "channel",his.channel},
                                                { "controller",his.controller},
                                                { "varKey",his.varKey},
                                                { "value",pubFun.IsSingle(his.value,0) }
                                            },
                                            Timestamp = DateTime.Now
                                        };
                                        points.Add(point);
                                    }
                                }
                                if (ServerConfig.DataBaseEnable)
                                {
                                    string connstring = $"server={ServerConfig.DbHost};user={ServerConfig.DbUser};database={ServerConfig.DbName};port={ServerConfig.DBPort};password={ServerConfig.DbPw};";
                                    ExecuteSqlTran(list, connstring);
                                }
                                if(ServerConfig.influxEnalble)
                                {
                                    writeinflux(influxdb, points);

                                }
                            }


                        }
                        catch(Exception ex)
                        {
                            Logger.GetInstance().LogError(ex.ToString());
                        }

                        TimeSpan endtime = new TimeSpan(DateTime.Now.Ticks);
                        TimeSpan span = endtime.Subtract(starttime).Duration();
                        double elapsedTime = span.TotalSeconds;//耗时
                        try
                        {
                            if (elapsedTime*1000 < 1000)//如果大于最小耗时就不延时了
                            {
                                if (SLEEP_TIME < 100) SLEEP_TIME = 100;
                                for (int i = 0; i < (int)((SLEEP_TIME-elapsedTime*1000 )/ 100); i++)
                                {
                                    if (!active)
                                    {
                                        thread = null;
                                        return;
                                    }
                                    System.Threading.Thread.Sleep(100);
                                }
                            }else
                                Thread.Sleep(100);
                        }
                        catch (System.Threading.ThreadInterruptedException)
                        {
                            throw;
                        }
                        catch { }
                    }
                    catch (System.Threading.ThreadInterruptedException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        if (onError != null)
                            onError(e);
                    }
                }
                thread = null;
            }catch(Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                thread = null;
            }
        }
      
        private string Get_tableName()
        {
            return "GH_History" + DateTime.Now.ToString("yyyyMMdd");
        }
        History GetHistory(IVariable var)
        {
            History his = new History
            {
                time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                server = ServerConfig.CloudClientID,
                channel = var.ControllerObject.ChannelObject.ID,
                controller = var.ControllerObject.ID,
                varKey = var.ID,
                value = var.Value.ToString()

            };
            return his;
        }
        void writeinflux(InfluxdbHelper influxdb, IChannel chan, IController con, IVariable item)
        {
            try
            {
               _=influxdb.Write(new Point
                {
                    Name = "gh_history", // serie/measurement/table to write into
                    Tags = new Dictionary<string, object>()
                        {
                           { "variable",ServerConfig.CloudClientID+":"+chan.ID+":"+con.ID+":"+item.ID },
                        },
                    Fields = new Dictionary<string, object>()
                        {
                            { "server",ServerConfig.CloudClientID},
                            { "channel",chan.ID},
                            { "controller",con.ID},
                            { "varKey",item.ID},
                            { "value",pubFun.IsSingle(item.Value.ToString(),0) }
                        },
                    Timestamp = DateTime.Now
                }, ServerConfig.influxName);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        void writeinflux(InfluxdbHelper influxdb,List<Point> points)
        {
            List<Point> points2 = new List<Point>();
            for (int n = 0; n < points.Count; n++)
            {
                points2.Add(points[n]);
                if (n > 0 && (n % 500 == 0 || n == points.Count - 1))
                {
                    
                    _ = influxdb.Write(points2.ToArray(), ServerConfig.influxName);
                    points2.Clear();
                }
            }
          
        }
        void writeinflux(InfluxdbHelper influxdb, History his)
        {
            try
            {
                Point point = new Point
                {
                    Name = "gh_history", // serie/measurement/table to write into
                    Tags = new Dictionary<string, object>()
                        {
                           { "variable",ServerConfig.CloudClientID+":"+his.channel+":"+his.controller+":"+his.varKey },
                        },
                    Fields = new Dictionary<string, object>()
                        {
                            { "server",ServerConfig.CloudClientID},
                            { "channel",his.channel},
                            { "controller",his.controller},
                            { "varKey",his.varKey},
                            { "value",pubFun.IsSingle(his.value,0) }
                        },
                    Timestamp = DateTime.Now
                };

               _= influxdb.Write(point, ServerConfig.influxName);




            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

           
        void sentToMqtt(IChannel chan, IController con, IVariable item)
        {
            History hs = new History();
            hs.channel = chan.ID;
            hs.controller = con.ID;
            hs.server = ServerConfig.CloudClientID;
            hs.varKey = item.ID;
            hs.value = item.Value.ToString();
            hs.time = item.DateStamp.ToString("yyyy/MM/dd HH:mm:ss");

            MqttController.dataLocalDB.Enqueue(hs);
        }
        
        public static void  VariableChangedRecord(History history)
        {
            History temp;
            if (varChangedRecord.Count > MaxChangeRecord)
                varChangedRecord.TryDequeue(out temp);
            varChangedRecord.Enqueue(history);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>sql2000数据库
        /// <param name="SQLStringList">多条SQL语句</param>
        public static void ExecuteSqlTran(List<string> SQLStringList, string ConnStr)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                        if (n > 0 && (n % 500 == 0 || n == SQLStringList.Count - 1))
                        {
                            tx.Commit();
                            tx = conn.BeginTransaction();
                            //原本是直接下面统一提交，听从sp1234的意见，就在这里重启事务，不知道这样写会不会/好，不过我这些写运行起来好像没问题。
                        }
                    }

                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }


    }
}
