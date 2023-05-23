using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using GHIBMS.Common;
using System.IO.Ports;
using System.IO;
using System.Xml;
using System.Diagnostics;
using GHIBMS.Common;
using System.Runtime.InteropServices;

namespace GHIBMS.Server
{
  
    public static class ProjectMng
    {

         #region 将数据库还原成链表

           public static void LoadChanList() //获取通道
           {
               try
               {
                   Rtdb.ChanList.Clear();
                   string sql = string.Format("select * from S_ChannelInfo");
                   DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                   DataTable dt= db.ExecuteQuery(sql);
                   if (dt != null)
                   {
                       foreach (DataRow dr in dt.Rows)
                       {
                           ProtocolCodeEnum ProtocolCode = (ProtocolCodeEnum)(dr["DeviceCode"] is DBNull ? 0 : Convert.ToInt32(dr["DeviceCode"].ToString()));

                           ChannelInfo chan = (ChannelInfo)PluginMng.CreateChannel(ProtocolCode);
                           chan.Name = dr["ChannelName"] is DBNull ? string.Empty : dr["ChannelName"].ToString();
                           chan.SysFunLabel = dr["DeviceSort"] is DBNull ? string.Empty : dr["DeviceSort"].ToString();
                           //chan.ProtocolName = dr["DeviceType"] is DBNull ? string.Empty : dr["DeviceType"].ToString();
                           //chan.ProtocolCode = ProtocolCode;
                           chan.CommType = (CommInterface)(dr["CommType"] is DBNull ? 0 : Convert.ToInt32(dr["CommType"].ToString()));
                           chan.NetProtocol = (NetProcotol)(dr["NetPotocol"] is DBNull ? 0 : Convert.ToInt32(dr["NetPotocol"].ToString()));
                           chan.NetPort = dr["NetPort"] is DBNull ? 3000 : Convert.ToInt32(dr["NetPort"].ToString());
                           chan.IpAddress = dr["IPaddress"] is DBNull ? string.Empty : dr["IPaddress"].ToString();
                           chan.Port = dr["SerialPort"] is DBNull ? 1 : Convert.ToInt32(dr["SerialPort"].ToString());
                           chan.Baud = dr["Baud"] is DBNull ? 9600 : Convert.ToInt32(dr["Baud"].ToString());
                           chan.DataBit = dr["Databit"] is DBNull ? 8 : Convert.ToInt32(dr["Databit"].ToString());
                           chan.StopBit = (StopBits)(dr["Stopbit"] is DBNull ? 1 : Convert.ToInt32(dr["Stopbit"].ToString()));
                           chan.Parity = (Parity)(dr["Parity"] is DBNull ? 0 : Convert.ToInt32(dr["Parity"].ToString()));
                           chan.Handshake = (Handshake)(dr["Handshake"] is DBNull ? 0 : Convert.ToInt32(dr["Handshake"].ToString()));
                           chan.OpcServerName = dr["OPCServerName"] is DBNull ? string.Empty : dr["OPCServerName"].ToString();
                           try
                           {
                               chan.Enable = dr["Enable"] is DBNull ? false : Convert.ToBoolean(dr["Enable"].ToString());
                           }
                           catch
                           {
                               chan.Enable = true;
                           }
                           chan.Description = dr["Desc"] is DBNull ? string.Empty : dr["Desc"].ToString();

                           chan.SendInterval = dr["SendInterval"] is DBNull ? 10 : Convert.ToInt32(dr["SendInterval"].ToString());

                           Rtdb.ChanList.Add(chan);
                       }
                   }
                   db.Close();
                   GetControllerInfoList();//通道查找完成，查找控制器
               }
               catch (Exception ex)
               {
                   Console.WriteLine(ex.ToString());
               }
           }

           private static void GetControllerInfoList( )//根据通道名称获取通道下面的控制器
           {
               try 
               {
                   DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                   foreach(ChannelInfo chan in Rtdb.ChanList)
                   {
                     string sql = string.Format("select * from S_ControllerInfo where ChannelName = '{0}'",chan.Name);
                     DataTable dt=db.ExecuteQuery(sql);
                     if (dt != null)
                     {
                         foreach (DataRow dr in dt.Rows)
                         {
                             ControllerInfo con = new ControllerInfo();
                             con.Name = dr["ControlName"] is DBNull ? string.Empty : dr["ControlName"].ToString();
                             con.SysFunLabel = dr["DeviceSort"] is DBNull ? string.Empty : dr["DeviceSort"].ToString();
                             con.Address = dr["ControllerAddress"] is DBNull ? 0 : Convert.ToInt32(dr["ControllerAddress"].ToString());
                             con.Description = dr["Desc"] is DBNull ? string.Empty : dr["Desc"].ToString();
                             con.IpAddress = dr["IPaddress"] is DBNull ? string.Empty : dr["IPaddress"].ToString();
                             con.Port = dr["Port"] is DBNull ? 0 : Convert.ToInt32(dr["Port"].ToString());
                             con.MacAddress = dr["MacAddress"] is DBNull ? string.Empty : dr["MacAddress"].ToString();
                             con.Enable = dr["Enable"] is DBNull ? false : Convert.ToBoolean(dr["Enable"].ToString());
                             con.UserName = dr["UserName"] is DBNull ? string.Empty : dr["UserName"].ToString();
                             con.PassWord = dr["PassWord"] is DBNull ? string.Empty : dr["PassWord"].ToString();
                             con.DeviceLabel = dr["DeviceDetail"] is DBNull ? string.Empty : dr["DeviceDetail"].ToString();
                             con.Rate = dr["OPCRate"] is DBNull ? 1000 : Convert.ToInt32(dr["OPCRate"].ToString());
                             con.TimeBias = dr["OPCTimeBias"] is DBNull ? 0 : Convert.ToInt32(dr["OPCTimeBias"].ToString());
                             con.DeadBand = dr["OPCDeadBand"] is DBNull ? 0 : Convert.ToInt32(dr["OPCDeadBand"].ToString());
                             con.LCID = dr["OPCLcid"] is DBNull ? 0 : Convert.ToInt32(dr["OPCLcid"].ToString());
                             con.InChannel = dr["InChannel"] is DBNull ? 0 : Convert.ToInt32(dr["InChannel"].ToString());
                             con.OutChannel = dr["OutChannel"] is DBNull ? 0 : Convert.ToInt32(dr["OutChannel"].ToString());
                             con.Area = dr["RegionName"] is DBNull ? string.Empty : dr["RegionName"].ToString();
                             con.ChannelObject = chan;
                             chan.ConList.Add(con);
                         }
                     }
                   }
                   db.Close();
                   GetPointList(); //获取控制器下面点的信息
               }
               catch (Exception ex)
               {
                   Console.WriteLine(ex.ToString());
               }
           }

           private static void GetPointList() //获取控制器下面点的信息
           {
               try
               {
                   DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                   foreach (ChannelInfo chan in Rtdb.ChanList)
                   {
                       foreach (ControllerInfo con in chan.ConList)
                       {
                           string sql = string.Format("select * from S_Point where ControlName = '{0}'", con.Name);
                           DataTable dt = db.ExecuteQuery(sql);
                           if (dt != null)
                           {
                               foreach (DataRow dr in dt.Rows)
                               {
                                   Variable point = new Variable();
                                   //point.Name = dr["ControlName"] is DBNull ? string.Empty : dr["ControlName"].ToString();
                                   point.Name = dr["PointName"] is DBNull ? string.Empty : dr["PointName"].ToString();
                                   point.Address = dr["PointAddress"] is DBNull ? string.Empty : dr["PointAddress"].ToString();
                                   point.Description = dr["PointDescription"] is DBNull ? string.Empty : dr["PointDescription"].ToString();
                                   point.Valuetype = (VarEnum)(dr["PointValueType"] is DBNull ? 0 : Convert.ToInt32(dr["PointValueType"].ToString()));
                                   point.Enable = dr["Enable"] is DBNull ? false : Convert.ToBoolean(dr["Enable"].ToString());
                                   point.bReadOnly = dr["ReadOnly"] is DBNull ? false : Convert.ToBoolean(dr["ReadOnly"].ToString());
                                   point.OperLevel = dr["Level"] is DBNull ? 0 : Convert.ToInt32(dr["Level"].ToString());
                                   point.SysFunLabel = dr["PointSort"] is DBNull ? string.Empty : dr["PointSort"].ToString();
                                   point.Unit = dr["Unit"] is DBNull ? string.Empty : dr["Unit"].ToString();
                                   point.AssociateVideo = dr["Associatevideo"] is DBNull ? string.Empty : dr["Associatevideo"].ToString();
                                   point.DeviceLabel = dr["DeviceDetail"] is DBNull ? string.Empty : dr["DeviceDetail"].ToString();
                                   point.AssociateForm = dr["AlarmForm"] is DBNull ? string.Empty : dr["AlarmForm"].ToString();
                                   point.Area = dr["RegionName"] is DBNull ? string.Empty : dr["RegionName"].ToString();
                                   point.ControllerObject = con;
                                   con.VarList.Add(point);
                               }
                           }
                       }
                   }
                   db.Close();

                   GetPointRelationList();
                 
               }
               catch (Exception ex)
               {
                   Console.WriteLine(ex.ToString());
               }
           }
          
           private static void GetPointRelationList() //获取点的联动关系
           {
               try
               {
                   DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);

                   foreach (ChannelInfo chan in Rtdb.ChanList)
                   {
                       foreach (ControllerInfo con in chan.ConList)
                       {
                           foreach (Variable var in con.VarList)
                           {
                               string sql = string.Format("select * from S_PointAction where PointName = '{0}'", var.Name);
                               DataTable dt = db.ExecuteQuery(sql);
                               if (dt != null)
                               {
                                   foreach (DataRow dr in dt.Rows)
                                   {
                                       VariableAction act = new VariableAction();
                                       act.Desc = dr["Desc"] is DBNull ? string.Empty : dr["Desc"].ToString();
                                       act.VarName = dr["TargetName"] is DBNull ? string.Empty : dr["TargetName"].ToString();
                                       act.VarValue = dr["PointValue"] is DBNull ? string.Empty : dr["PointValue"].ToString();
                                       act.Way = dr["AlarmWay"] is DBNull ? string.Empty : dr["AlarmWay"].ToString();
                                       var.ActionList.Add(act);
                                   }
                               }

                               string sql1 = string.Format("select * from S_PointVideoAction where PointName = '{0}'", var.Name);
                               DataTable dt1 = db.ExecuteQuery(sql1);
                               if (dt1 != null)
                               {
                                   foreach (DataRow dr in dt1.Rows)
                                   {
                                       VariableVideo vdo = new VariableVideo();
                                       vdo.CamID = dr["CamID"] is DBNull ? 0 : Convert.ToInt32(dr["CamID"].ToString());
                                       vdo.MatrixName = dr["MatrixName"] is DBNull ? "" : dr["MatrixName"].ToString();
                                       vdo.MonID = dr["MonID"] is DBNull ? 0 : Convert.ToInt32(dr["MonID"].ToString());
                                       vdo.Way = dr["AlarmWay"] is DBNull ? string.Empty : dr["AlarmWay"].ToString();
                                       vdo.PreSetID = dr["PreSetID"] is DBNull ? 0 : Convert.ToInt32(dr["PreSetID"].ToString());
                                       var.VideoList.Add(vdo);//同时把视频所对应的联动关系加入对应点的视频列表下
                                   }
                               }

                               string sql2 = string.Format("select * from S_PointSMS where PointName = '{0}'", var.Name);
                               DataTable dt2 = db.ExecuteQuery(sql2);
                               if (dt2 != null)
                               {

                                   foreach (DataRow dr in dt2.Rows)
                                   {
                                       VariableSMS sms = new VariableSMS();
                                       sms.Msg = dr["Msg"] is DBNull ? string.Empty : dr["Msg"].ToString();
                                       sms.Phone = dr["Phone"] is DBNull ? string.Empty : dr["Phone"].ToString();
                                       sms.Way = dr["AlarmWay"] is DBNull ? string.Empty : dr["AlarmWay"].ToString();
                                       var.SmsList.Add(sms);//把该短信对应的关系加入对应的点下
                                   }
                               }

                               string sql3 = string.Format("select * from S_PointDesc where PointName = '{0}'", var.Name);
                               DataTable dt3 = db.ExecuteQuery(sql3);
                               if (dt3 != null)
                               {
                                   foreach (DataRow dr in dt3.Rows)
                                   {
                                       VariableDesc desc = new VariableDesc();
                                       desc.StateDesc = dr["StateDes"] is DBNull ? string.Empty : dr["StateDes"].ToString();
                                       desc.StateValue = dr["StateValue"] is DBNull ? string.Empty : dr["StateValue"].ToString();
                                       var.DescList.Add(desc);
                                   }
                               }

                               string sql4 = string.Format("select * from S_PointAlarmWay where PointName = '{0}'", var.Name);
                               DataTable dt4 = db.ExecuteQuery(sql4);
                               if (dt4 != null)
                               {
                                   foreach (DataRow dr in dt4.Rows)
                                   {
                                       VariableWay way = new VariableWay();
                                       way.AlarmDesc = dr["AlarmDesc"] is DBNull ? string.Empty : dr["AlarmDesc"].ToString();
                                       way.Express = dr["AlarmWay"] is DBNull ? string.Empty : dr["AlarmWay"].ToString();
                                       way.Priority = dr["Priority"] is DBNull ? 1 : Convert.ToUInt32(dr["Priority"].ToString());
                                       var.WayList.Add(way);//把该报警条件加入到该点所对应的列表下
                                   }
                               }
                               if (var.DeviceLabel == (DeviceLabelEnum.摄像机.ToString()))
                               {
                                   VariableCam cam = new VariableCam();
                                   var.CamEx = cam;
                                   string sql5 = string.Format("select * from S_PointCamera where PointName = '{0}'", var.Name);
                                   DataTable dt5 = db.ExecuteQuery(sql5);
                                   if (dt5 != null)
                                   {
                                       if (dt5.Rows.Count > 0)
                                       {
                                           var.CamEx.MatrixName = dt5.Rows[0]["MatrixName"] is DBNull ? string.Empty : dt5.Rows[0]["MatrixName"].ToString();
                                           var.CamEx.MatrixInchannel = dt5.Rows[0]["MatrixCh"] is DBNull ? 0 : pubFun.IsInt(dt5.Rows[0]["MatrixCh"].ToString(),1);
                                           var.CamEx.DvrName = dt5.Rows[0]["DvrName"] is DBNull ? string.Empty : dt5.Rows[0]["DvrName"].ToString().Trim();
                                           var.CamEx.DvrInchannel = dt5.Rows[0]["DvrCh"] is DBNull ? 0 : pubFun.IsInt(dt5.Rows[0]["DvrCh"].ToString().Trim(),1);
                                           var.CamEx.VodName = dt5.Rows[0]["VodName"] is DBNull ? string.Empty : dt5.Rows[0]["VodName"].ToString().Trim();
                                           var.CamEx.UseMainCodeStream = dt5.Rows[0]["BitStream"] is DBNull ? false : Convert.ToBoolean(dt5.Rows[0]["BitStream"].ToString());
                                           var.CamEx.UseVodStream = dt5.Rows[0]["MediaStream"] is DBNull ? false : Convert.ToBoolean(dt5.Rows[0]["MediaStream"].ToString());


                                       }
                                   }
                               }
                           }
                       }
                   }
                   db.Close();
               }
               catch (Exception ex)
               {
                   Console.WriteLine(ex.ToString());
               }
           }
          #endregion

         #region 将链表写入数据库

           public static void SaveChanList()
           {
         
               ProjectMng.ClearDataBase();     //清空表
               ProjectMng.InsertIntoChannelInfo();   //写入通道信息
               ProjectMng.InsertIntoControllerInfo();//写入控制器信息
               ProjectMng.InsertIntoPoint();         //写入点的信息 
              
           }

           private static void InsertIntoChannelInfo()
          {
              try
              {
                  DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                  if (db == null) return;
                  foreach (ChannelInfo chan in Rtdb.ChanList)
                  {
                      string sql = string.Format("insert into  S_ChannelInfo (ChannelName,DeviceSort,DeviceType,DeviceCode,CommType,NetPotocol,NetPort,IPaddress,SerialPort,Baud,Databit,Stopbit,Parity,Handshake,OPCServerName,[Enable],[Desc],SendInterval) values (N'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',N'{14}','{15}',N'{16}',{17})",
                            chan.Name,
                            chan.SysFunLabel,
                            chan.ProtocolName,
                            (int)chan.ProtocolCode,
                            (int)chan.CommType,
                            (int)chan.NetProtocol,
                            chan.NetPort,
                            chan.IpAddress,
                            chan.Port,
                            chan.Baud,
                            chan.DataBit,
                            (int)chan.StopBit,
                            (int)chan.Parity,
                            (int)chan.Handshake,
                            chan.OpcServerName,
                            chan.Enable == false ? 0 : 1,
                            chan.Description,
                            chan.SendInterval
                            );

                      
                       db.ExecuteNonQuery(sql);
                  }
                  db.Close();
                 
              }
              catch (Exception ex)
              {
                 Logger.GetInstance().LogError(ex.ToString());
              }
          }

           private static void InsertIntoControllerInfo()
          {
              try
              {
                  DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                  if (db == null) return;
                  foreach (ChannelInfo chan in Rtdb.ChanList)
                  {
                      foreach (ControllerInfo con in chan.ConList)
                      {
                          string sql1 = string.Format("insert into  S_ControllerInfo (DeviceSort,ChannelName,ControllerAddress,[Desc],IPaddress,Port,MacAddress,Enable,UserName,PassWord,DeviceDetail,DeviceCode,OPCRate,OPCTimeBias,OPCDeadBand,OPCLcid,InChannel,OutChannel,ControlName,RegionName) values (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}')",
                               con.SysFunLabel,
                               con.ChannelObject.Name,
                               con.Address,
                               con.Description,
                               con.IpAddress,
                               con.Port,
                               con.MacAddress,
                               con.Enable == false ? 0 : 1,
                               con.UserName,
                               con.PassWord,
                               con.DeviceLabel,
                               pubFun.GetDeviceCode(con.DeviceLabel),
                               con.Rate,
                               con.TimeBias,
                               con.DeadBand,
                               con.LCID,
                               con.InChannel,
                               con.OutChannel,
                               con.Name,
                               con.Area
                              );
                          db.ExecuteNonQuery(sql1);
                      }
                  }
                  db.Close();
              }
              catch (Exception ex)
              {
                  Logger.GetInstance().LogError(ex.ToString());
              }
          }

           private static void InsertIntoPoint()
          {
              try
              {
                  DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                  if (db == null) return;
                  foreach (ChannelInfo chan in Rtdb.ChanList)
                  {
                      foreach (ControllerInfo con in chan.ConList)
                      {
                          foreach (Variable Point in con.VarList)
                          {
                              string sql2 = string.Format("insert into  S_Point (ControlName,PointName,PointAddress,PointDescription,PointValueType,Enable,ReadOnly,[Level],PointSort,Unit,Associatevideo,DeviceDetail,DeviceCode,AlarmForm,RegionName) values (N'{0}',N'{1}',N'{2}','{3}','{4}','{5}','{6}','{7}','{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}')",
                                  Point.ControllerObject.Name,
                                  Point.Name,
                                  Point.Address,
                                  Point.Description,
                                  (int)Point.Valuetype,
                                  Point.Enable == false ? 0 : 1,
                                  Point.bReadOnly == false ? 0 : 1,
                                  Point.OperLevel,
                                  Point.SysFunLabel,
                                  Point.Unit,
                                  Point.AssociateVideo,
                                  Point.DeviceLabel,
                                  pubFun.GetDeviceCode(Point.DeviceLabel),
                                  Point.AssociateForm,
                                  Point.Area
                                  );

                              db.ExecuteNonQuery(sql2);
                              //保存点的联动关系
                              //点的联锁关系
                              foreach (VariableAction VA in Point.ActionList)
                              {
                                  string sql3 = string.Format("insert into  S_PointAction(PointName,AlarmWay,[Desc],PointValue,TargetName) values ('{0}','{1}','{2}','{3}','{4}')",
                                      Point.Name,
                                      VA.Way,
                                      VA.Desc,
                                      VA.VarValue,
                                      VA.VarName
                                      );
                                  db.ExecuteNonQuery(sql3);
                              }
                              //点的视频联动
                              foreach (VariableVideo VV in Point.VideoList)
                              {
                                  string sql4 = string.Format("insert into  S_PointVideoAction (PointName,AlarmWay,MatrixName,MonID,CamID,PreSetID) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                                       Point.Name,
                                       VV.Way,
                                       VV.MatrixName,
                                       VV.MonID,
                                       VV.CamID,
                                       VV.PreSetID
                                      );
                                  db.ExecuteNonQuery(sql4);
                              }
                              //点的短信发送
                              foreach (VariableSMS VS in Point.SmsList)
                              {
                                  string sql5 = string.Format("insert into  S_PointSMS (PointName,AlarmWay,Phone,Msg) values ('{0}','{1}','{2}','{3}')",
                                      Point.Name,
                                      VS.Way,
                                      VS.Phone,
                                      VS.Msg
                                      );
                                  db.ExecuteNonQuery(sql5);

                              }
                              //点的自定义报警条件
                              foreach (VariableWay VW in Point.WayList)
                              {
                                  string sql7 = string.Format("insert into  S_PointAlarmWay (PointName,AlarmWay,AlarmDesc,Priority) values ('{0}','{1}','{2}','{3}')",
                                      Point.Name,
                                      VW.Express,
                                      VW.AlarmDesc,
                                      VW.Priority
                                      );
                                  db.ExecuteNonQuery(sql7);
                              }
                              //点的数值描述
                              foreach (VariableDesc VD in Point.DescList)
                              {
                                  string sql6 = string.Format("insert into  S_PointDesc ( PointName,StateValue,StateDes) values ('{0}','{1}','{2}')",
                                      Point.Name,
                                      VD.StateValue,
                                      VD.StateDesc
                                      );
                                  db.ExecuteNonQuery(sql6);

                              }
                              //摄像机
                              if (Point.DeviceLabel == (DeviceLabelEnum.摄像机.ToString()) && Point.CamEx != null)
                              {
                                  string sql7 = string.Format("insert into  S_PointCamera ( PointName,MatrixName,MatrixCh,DvrName,DvrCh,VodName,BitStream,MediaStream) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                         Point.Name,
                                         Point.CamEx.MatrixName,
                                         Point.CamEx.MatrixInchannel,
                                         Point.CamEx.DvrName,
                                         Point.CamEx.DvrInchannel,
                                         Point.CamEx.VodName,
                                         Point.CamEx.UseMainCodeStream,
                                         Point.CamEx.UseVodStream
                                         );
                                  db.ExecuteNonQuery(sql7);
                              }
                           
                          }
                      }
                  } 
                  db.Close();
              }
              catch (Exception ex)
              {
                  Logger.GetInstance().LogError(ex.ToString());
              }
          }

          //清除数据通道、控制器，点表
          private static void ClearDataBase()
          {
              try
              {
                  string sql = string.Format("TRUNCATE table  S_ChannelInfo  TRUNCATE table  S_ControllerInfo  TRUNCATE table  S_Point  TRUNCATE table  S_PointAction  TRUNCATE table  S_PointAlarmWay  TRUNCATE table  S_PointDesc    TRUNCATE table  S_PointSMS  TRUNCATE table  S_PointVideoAction  TRUNCATE table  S_PointCamera");
                  DBManager dbm = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                  if (dbm != null)
                  {
                      dbm.ExecuteNonQuery(sql);
                      dbm.Close();
                  }
              }
              catch (Exception ex) 
              {
                  Logger.GetInstance().LogError(ex.ToString());
              }
          }
           #endregion

         #region 图元写入数据库
         public static void SaveGraphicList(List<GraphicObjectMngr> FormList)
         {
             try
             {
                 string sql = string.Format("TRUNCATE table  P_GraphicObject  TRUNCATE table  P_GraphObjectMngr ");
                 DBManager dbm = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                 if (dbm != null)
                 {
                     dbm.ExecuteNonQuery(sql);
                     foreach (GraphicObjectMngr form in FormList)
                     {
                         string sql1 = string.Format("insert into  P_GraphObjectMngr (formname,projectName) values ('{0}','{1}')",
                               form.FormName,
                               ""//项目名称
                               );
                         dbm.ExecuteNonQuery(sql1);
                         
                     }
                     foreach (GraphicObjectMngr form in FormList)
                     {
                         foreach (GraphicObject g in form.GraphicObjectsList)
                         {
                             string sql2 = string.Format("insert into  P_GraphicObject (ControlID,ControlClass,AssociatePoint,ViewFormat,FormName) values ('{0}','{1}','{2}','{3}','{4}')",
                                   g.controlID,
                                   g.controlClass,
                                   g.associateVar,
                                  (int)g.ViewFormat,
                                  form.FormName
                                   );
                             dbm.ExecuteNonQuery(sql2);
                             
                         }
                     }
                     dbm.Close();
                 }
               
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }

         
         }
        #endregion

         #region 将数据库中的信息还原成链表
         public static void ResoreFormList(List<GraphicObjectMngr> formlist)
         {
             try
             {
                 formlist.Clear();
                 string sql = string.Format("select * from P_GraphObjectMngr");
                 DBManager db = DBManager.GetInstance(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw);
                 DataTable dt = db.ExecuteQuery(sql);
                 if (dt != null)
                 {
                     foreach (DataRow dr in dt.Rows)
                     {
                         GraphicObjectMngr gr = new GraphicObjectMngr();
                         gr.FormName = dr["formname"] is DBNull ? string.Empty : dr["formname"].ToString();
                         gr.ProjectName = dr["projectName"] is DBNull ? string.Empty : dr["projectName"].ToString();
                         formlist.Add(gr);

                     }
                 }
                 foreach (GraphicObjectMngr form in formlist)
                   {
                       string sql1 = string.Format("select * from P_GraphicObject where FormName = '{0}'",form.FormName);
                       DataTable dt1 = db.ExecuteQuery(sql1);
                       if (dt1 != null)
                       {
                           foreach (DataRow dr in dt1.Rows)
                           {
                               GraphicObject g = new GraphicObject();
                               g.controlID = dr["ControlID"] is DBNull ? string.Empty : dr["ControlID"].ToString();
                               g.controlClass = dr["ControlClass"] is DBNull ? string.Empty : dr["ControlClass"].ToString();
                               g.associateVar = dr["AssociatePoint"] is DBNull ? string.Empty : dr["AssociatePoint"].ToString();
                               g.ViewFormat = (DataViewFormatEnum)(dr["ViewFormat"] is DBNull ? 0 : Convert.ToInt32(dr["ViewFormat"].ToString()));
                               form.GraphicObjectsList.Add(g);
                           }
                       }
                   }
                 db.Close();
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }
         }
        #endregion

 
    }
}
