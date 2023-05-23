using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using GHIBMS.Common;
using GHIBMS.Common;
using GHIBMS.Interface;
namespace GHIBMS.Server
{
     public class RtdbMng
    {
        #region RTDB->XML
        public static void SaveToXml(string FileName)
        {
            try
            {
                //创建XmlDocument文档
                XmlDocument doc = new XmlDocument();
                //创建根元素
                doc.LoadXml("<Channels></Channels>");
                XmlNode root = doc.DocumentElement;
                doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);

                foreach (BaseChannel chan in Rtdb.ChanList)
                {  
                    ICommunicationPlug plug= chan.Plugin;
                    XmlElement xmlChan=plug.SaveChannel(doc,chan);
 ;
                    root.AppendChild(xmlChan);
                    foreach (ControllerInfo con in chan.ConList)
                    {
                        XmlElement xmlCon = XmlHelper<ControllerInfo>.EntityToXml(doc, con,con.Name);
                        xmlChan.AppendChild(xmlCon);
                        foreach (Variable var in con.VarList)
                        {

                            XmlElement xmlVar = XmlHelper<Variable>.EntityToXml(doc, var,var.Name);
                            xmlCon.AppendChild(xmlVar);

                            if (var.CamEx != null)
                            {
                                XmlElement xmlCam = XmlHelper<VariableCam>.EntityToXml(doc, var.CamEx);
                                xmlVar.AppendChild(xmlCam);
                            }

                            foreach (VariableDesc vd in var.DescList)
                            {
                                XmlElement xmlDesc = XmlHelper<VariableDesc>.EntityToXml(doc, vd);
                                xmlVar.AppendChild(xmlDesc);
                            }
                            foreach (VariableWay way in var.WayList)
                            {
                                XmlElement xmlway = XmlHelper<VariableWay>.EntityToXml(doc, way);
                                xmlVar.AppendChild(xmlway);
                            }
                            foreach (VariableAction act in var.ActionList)
                            {
                                XmlElement xmlact = XmlHelper<VariableAction>.EntityToXml(doc, act);
                                xmlVar.AppendChild(xmlact);
                            }
                            foreach (VariableVideo vdo in var.VideoList)
                            {
                                XmlElement xmlvdo = XmlHelper<VariableVideo>.EntityToXml(doc, vdo);
                                xmlVar.AppendChild(xmlvdo);
                            }
                            foreach (VariableSMS sms in var.SmsList)
                            {
                                XmlElement xmlvdo = XmlHelper<VariableSMS>.EntityToXml(doc, sms);
                                xmlVar.AppendChild(xmlvdo);
                            }
                        }
                    }
                }
                doc.Save(FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        #endregion

        #region XML->RTDB
        public static void LoadFromXml(string FileName)
        {
            try
            {
                Rtdb.ChanList.Clear();
                BaseChannel chan;
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(FileName);

                foreach (ICommunicationPlug plug in PluginMng.CommPlugs)
                {
                    #region Create channel
                    XmlNodeList xmlChans = xmlDoc.SelectNodes("/Channels/"+plug.PlugID);
                    foreach (XmlNode chNode in xmlChans)
                    {
                        chan = (BaseChannel)plug.CreateChannel(chNode);
                        Rtdb.ChanList.Add(chan);

                        XmlNodeList xmlCons = chNode.SelectNodes("ControllerInfo");
                        foreach (XmlNode conNode in xmlCons)
                        {
                            //反序列化控制器
                            ControllerInfo con = XmlHelper<ControllerInfo>.XmlNodeToEntity(conNode);
                            chan.ConList.Add(con);

                            con.ChannelObject = chan;

                            XmlNodeList xmlVars = conNode.SelectNodes("Variable");
                            foreach (XmlNode varNode in xmlVars)
                            {
                                //反序列化点
                                Variable var = XmlHelper<Variable>.XmlNodeToEntity(varNode);
                                con.VarList.Add(var);
                                var.ControllerObject = con;
                                //关连的摄像机
                                XmlNode xmlCam = varNode.SelectSingleNode("VariableCam");
                                if (xmlCam != null)
                                {
                                    VariableCam cam = XmlHelper<VariableCam>.XmlNodeToEntity(xmlCam);
                                    var.CamEx = cam;
                                }
                                //点的描述
                                XmlNodeList xmlDescs = varNode.SelectNodes("VariableDesc");
                                foreach (XmlNode varDesc in xmlDescs)
                                {
                                    VariableDesc desc = XmlHelper<VariableDesc>.XmlNodeToEntity(varDesc);
                                    var.DescList.Add(desc);
                                }
                                //点的报警条件
                                XmlNodeList xmlWays = varNode.SelectNodes("VariableWay");
                                foreach (XmlNode xmlWay in xmlWays)
                                {
                                    VariableWay way = XmlHelper<VariableWay>.XmlNodeToEntity(xmlWay);
                                    var.WayList.Add(way);
                                }
                                //点的动作
                                XmlNodeList xmlActs = varNode.SelectNodes("VariableAction");
                                foreach (XmlNode xmlAct in xmlActs)
                                {
                                    VariableAction act = XmlHelper<VariableAction>.XmlNodeToEntity(xmlAct);
                                    var.ActionList.Add(act);
                                }
                                //点的矩阵联动
                                XmlNodeList xmlVdos = varNode.SelectNodes("VariableVideo");
                                foreach (XmlNode xmlVdo in xmlVdos)
                                {
                                    VariableVideo vdo = XmlHelper<VariableVideo>.XmlNodeToEntity(xmlVdo);
                                    var.VideoList.Add(vdo);
                                }
                                //点的短信报警
                                XmlNodeList xmlSmss = varNode.SelectNodes("VariableSMS");
                                foreach (XmlNode xmlSms in xmlSmss)
                                {
                                    VariableSMS vdo = XmlHelper<VariableSMS>.XmlNodeToEntity(xmlSms);
                                    var.SmsList.Add(vdo);
                                }
                            }
                        }
                    }//end foreach chan
                    #endregion

                }
             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        #endregion

       
    }
}
