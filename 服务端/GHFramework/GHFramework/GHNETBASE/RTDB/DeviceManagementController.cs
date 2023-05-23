using GH.RTDB.Server.Common;
using GH.RTDB.Server.Models;
using GHCore;
using GHCore.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GH.RTDB.Server.Controllers
{
    public class DeviceManagementController : StatusController
    {
        //
        // POST: /DeviceManagement/
        [HttpPost]

        public ActionResult LoadChlAndCtrl()
        {
            try
            {
                var ak = (User.Identity as System.Security.Claims.ClaimsIdentity).FindFirst("GH:UUID").Value;

                var authkey = Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });

                var chlkeys = DeviceManagement.SingletonInstance.ChlGetAll(authkey).ToList();
                var chlvalues = DeviceManagement.SingletonInstance.ChlGetValues(authkey, chlkeys);
                var chlModels = chlvalues.Select(t=>JSONFormatter.Deserialize<DevChannel>(t));

                List<string> ctrlvalues = new List<string>();
                foreach (string chlk in chlkeys)
                {
                    var ctrlkeys = DeviceManagement.SingletonInstance.CtrlGetAll(authkey, chlk).ToList();
                    if(ctrlkeys.Count>0)
                    ctrlvalues.AddRange( DeviceManagement.SingletonInstance.CtrlGetValues(authkey, chlk, ctrlkeys));
                }
                var ctrlModels = ctrlvalues.Select(t => JSONFormatter.Deserialize<DevController>(t));

                var qu = chlModels.OrderBy(o=>o.ID).Select(t => new
                                            {
                                                text = t.Name+"("+t.ID+")",
                                                href = "#" + t.ID,
                                                key = t.ID,
                                                pkey = "",
                                                tags = new string[] { ctrlModels.Count(c => c.ChlID == t.ID).ToString() },
                                                nodes = ctrlModels.Where(w => w.ChlID == t.ID).OrderBy(o => o.ID).Select(cc => new 
                                                                                                        {
                                                                                                            text = cc.Name + "(" + cc.ID + ")",
                                                                                                            href = "#" + cc.ID,
                                                                                                            key = cc.ID,
                                                                                                            pkey = cc.ChlID,
                                                                                                            icon = "glyphicon glyphicon-earphone"
                                                                                                            //tags = 不查询变量数量
                                                                                                        })
                                            });

                return Json(new { success = true, data = qu.ToList() });
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AddChlOrCtrl()
        {
            try
            {
                var authkey = Request["authkey"];
                if(string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });
                var type = Request["chlType"];
                if (string.IsNullOrEmpty(type))
                    return Json(new { success = false, message = "请选择类型" });
                var key = Request["key"];
                if (string.IsNullOrEmpty(key))
                    return Json(new { success = false, message = "key唯一键不能为空" });
                var pkey = Request["pkey"];
                var name = Request["name"];
                if (string.IsNullOrEmpty(name))
                    return Json(new { success = false, message = "名称不能为空" });


                bool ret = false;
                if (type == "1")//通道
                { 
                    string value = JSONFormatter.Serialize(new DevChannel{ID=key, Name= name});
                    ret = DeviceManagement.SingletonInstance.ChlAdd(authkey, key, value );
                }
                else if (type == "2")//控制器
                {
                    if (string.IsNullOrEmpty(pkey))
                        return Json(new { success = false, message = "未选择通道" });

                    string value = JSONFormatter.Serialize(new DevController { ID = key, Name = name, ChlID= pkey });
                    ret = DeviceManagement.SingletonInstance.CtrlAdd(authkey, pkey, key, value);
                }
                if(!ret)
                    return Json(new { success = false, message = "操作失败！" });
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message=ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateChlOrCtrl()
        {
            try
            {
                var authkey = Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });
                var type = Request["chlType"];
                if (string.IsNullOrEmpty(type))
                    return Json(new { success = false, message = "请选择类型" });
                var key = Request["key"];
                if (string.IsNullOrEmpty(key))
                    return Json(new { success = false, message = "key唯一键不能为空" });
                var pkey = Request["pkey"];
                var name = Request["name"];
                if (string.IsNullOrEmpty(name))
                    return Json(new { success = false, message = "名称不能为空" });


                bool ret = false;
                if (type == "1")//通道
                {
                    string value = JSONFormatter.Serialize(new DevChannel { ID = key, Name = name });
                    ret = DeviceManagement.SingletonInstance.ChlUpdate(authkey, key, value);
                }
                else if (type == "2")//控制器
                {
                    if (string.IsNullOrEmpty(pkey))
                        return Json(new { success = false, message = "未选择通道" });

                    string value = JSONFormatter.Serialize(new DevController { ID = key, Name = name, ChlID = pkey });
                    ret = DeviceManagement.SingletonInstance.CtrlUpdate(authkey, pkey, key, value);
                }
                if (!ret)
                    return Json(new { success = false, message = "操作失败！" });
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteChlOrCtrl()
        {
            try
            {
                var authkey = Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });
                var type = Request["chlType"];
                if (string.IsNullOrEmpty(type))
                    return Json(new { success = false, message = "请选择类型" });
                var key = Request["key"];
                if (string.IsNullOrEmpty(key))
                    return Json(new { success = false, message = "key唯一键不能为空" });
                var pkey = Request["pkey"];


                bool ret = false;
                if (type == "1")//通道
                {
                    ret = DeviceManagement.SingletonInstance.ChlRemove(authkey, key);
                }
                else if (type == "2")//控制器
                {
                    if (string.IsNullOrEmpty(pkey))
                        return Json(new { success = false, message = "未选择通道" });

                    ret = DeviceManagement.SingletonInstance.CtrlRemove(authkey, pkey, key);
                }
                if (!ret)
                    return Json(new { success = false, message = "操作失败！" });
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult AddVariable()
        {
            try
            {
                var authkey = Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });
                var chlkey = Request["chlkey"];
                if (string.IsNullOrEmpty(chlkey))
                    return Json(new { success = false, message = "通道key不能为空" });
                var ctrlkey = Request["ctrlkey"];
                if (string.IsNullOrEmpty(ctrlkey))
                    return Json(new { success = false, message = "控制器key不能为空" });
                var key = Request["key"];
                if (string.IsNullOrEmpty(key))
                    return Json(new { success = false, message = "ke不能为空" });
                var name = Request["name"];
                if (string.IsNullOrEmpty(name))
                    return Json(new { success = false, message = "名称不能为空" });
                var val = Request["value"];
                var valType = GH.RTDB.Server.Models.ValueType.String;
                Enum.TryParse<GH.RTDB.Server.Models.ValueType>(Request["valueType"], out valType);
                var status = int.Parse(Request["status"]);


                bool ret = false;
                string value = JSONFormatter.Serialize(new DevVariable { ID = key, Name = name, ChlID = chlkey, CtrlID = ctrlkey, Value = val, ValueType = valType, Timestamp = DateTime.Now, Status =status, Area="1", Category="1", Level=0 });
                ret = DeviceManagement.SingletonInstance.VarAdd(authkey, chlkey, ctrlkey, key, value);

                if (!ret)
                    return Json(new { success = false, message = "操作失败！" });
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateVariable()
        {
            try
            {
                var authkey = Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });
                var chlkey = Request["chlkey"];
                if (string.IsNullOrEmpty(chlkey))
                    return Json(new { success = false, message = "通道key不能为空" });
                var ctrlkey = Request["ctrlkey"];
                if (string.IsNullOrEmpty(ctrlkey))
                    return Json(new { success = false, message = "控制器key不能为空" });
                var key = Request["key"];
                if (string.IsNullOrEmpty(key))
                    return Json(new { success = false, message = "key不能为空" });
                var name = Request["name"];
                if (string.IsNullOrEmpty(name))
                    return Json(new { success = false, message = "名称不能为空" });
                var val = Request["value"];
                var valType = GH.RTDB.Server.Models.ValueType.String;
                Enum.TryParse<GH.RTDB.Server.Models.ValueType>(Request["valueType"], out valType);
                var status = int.Parse(Request["status"]);

                bool ret = false;
                string value = JSONFormatter.Serialize(new DevVariable { ID = key, Name = name, ChlID = chlkey, CtrlID = ctrlkey, Value = val, ValueType = valType, Timestamp = DateTime.Now, Status = status, Area = "1", Category = "1", Level = 0 });
                ret = DeviceManagement.SingletonInstance.VarUpdate(authkey, chlkey, ctrlkey, key, value);

                if (!ret)
                    return Json(new { success = false, message = "操作失败！" });
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteVariable()
        {
            try
            {
                var authkey = Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });

                var data = Request["data"];
                var query = JsonConvert.DeserializeObject<List<DevVariable>>(data);

                bool ret = false;
                foreach (DevVariable entity in query)
                {
                    ret = DeviceManagement.SingletonInstance.VarRemove(authkey, entity.ChlID, entity.CtrlID, entity.ID);
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult GetVariableList()
        {
            try
            {
                var authkey = HttpContext.Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { data = new List<object>() }, JsonRequestBehavior.AllowGet);
                var chlkey = HttpContext.Request["chlkey"];
                if (string.IsNullOrEmpty(chlkey))
                    return Json(new { data = new List<object>() }, JsonRequestBehavior.AllowGet);
                var ctrlkey = HttpContext.Request["ctrlkey"];
                if (string.IsNullOrEmpty(ctrlkey))
                    return Json(new { data = new List<object>() }, JsonRequestBehavior.AllowGet);
                
                int start, length, sEcho;
                sEcho = 0;
                if (!string.IsNullOrEmpty(HttpContext.Request["iDisplayStart"]))
                {
                    start = int.Parse(HttpContext.Request["iDisplayStart"]);
                    length = int.Parse(HttpContext.Request["iDisplayLength"]);
                    if (length <= 0) length = 10;
                    sEcho = int.Parse(HttpContext.Request["sEcho"]);
                }
                else
                {
                    int page = 1;
                    int.TryParse(HttpContext.Request["page"], out page);
                    length = 10;
                    int.TryParse(HttpContext.Request["length"], out length);
                    start = (page - 1) * length;
                }

                var varkeys = DeviceManagement.SingletonInstance.VarGetAll(authkey, chlkey, ctrlkey).ToList();
                var varvalues = DeviceManagement.SingletonInstance.VarGetValues(authkey, chlkey, ctrlkey, varkeys);
                var varModels = varvalues.Select(t => JSONFormatter.Deserialize<DevVariable>(t));

                var query = varModels.OrderBy(o => o.ID).Select(t => new List<object>()
                {
                    "",
                    t.ID,
                    t.Name,
                    t.Value,
                    t.ChlID,
                    t.CtrlID,
                    (int)t.ValueType,
                    t.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    t.Status
                });

                var count = query.Count();
                return Json(new
                {
                    sEcho = sEcho,
                    iTotalRecords = count,
                    iTotalDisplayRecords = count,
                    data = query.Skip(start).Take(length)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { data = new List<object>() }, JsonRequestBehavior.AllowGet);
            }

            //return Json(new { data = ls }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllVariableList()
        {
            try
            {
                var authkey = HttpContext.Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { data = new List<object>() }, JsonRequestBehavior.AllowGet);

                int start, length, sEcho;
                sEcho = 0;
                if (!string.IsNullOrEmpty(HttpContext.Request["iDisplayStart"]))
                {
                    start = int.Parse(HttpContext.Request["iDisplayStart"]);
                    length = int.Parse(HttpContext.Request["iDisplayLength"]);
                    if (length <= 0) length = 10;
                    sEcho = int.Parse(HttpContext.Request["sEcho"]);
                }
                else
                {
                    int page = 1;
                    int.TryParse(HttpContext.Request["page"], out page);
                    length = 10;
                    int.TryParse(HttpContext.Request["length"], out length);
                    start = (page - 1) * length;
                }

                var chlkeys = DeviceManagement.SingletonInstance.ChlGetAll(authkey).ToList();
                var chlvalues = DeviceManagement.SingletonInstance.ChlGetValues(authkey, chlkeys);//所有通道
                var chlModels = chlvalues.Select(t => JSONFormatter.Deserialize<DevChannel>(t));

                List<string> ctrlvalues = new List<string>();
                List<string> varvalues = new List<string>();
                foreach (string chlk in chlkeys)
                {
                    var ctrlkeys = DeviceManagement.SingletonInstance.CtrlGetAll(authkey, chlk).ToList();
                    foreach (string ctrlk in ctrlkeys)
                    {
                        var varkeys = DeviceManagement.SingletonInstance.VarGetAll(authkey, chlk, ctrlk).ToList();
                        var varvls = DeviceManagement.SingletonInstance.VarGetValues(authkey, chlk, ctrlk, varkeys);
                        varvalues.AddRange(varvls);
                    }
                    ctrlvalues.AddRange(DeviceManagement.SingletonInstance.CtrlGetValues(authkey, chlk, ctrlkeys));
                }
                var ctrlModels = ctrlvalues.Select(t => JSONFormatter.Deserialize<DevController>(t));//所有控制器
                var varModels = varvalues.Select(t => JSONFormatter.Deserialize<DevVariable>(t));

                var query = varModels.OrderBy(o => o.ID).Select(t => new 
                {
                    ID = t.ID,
                    Name = t.Name,
                    Value = t.Value,
                    ChlID = t.ChlID,
                    CtrlID = t.CtrlID,
                    ChlName = chlModels.FirstOrDefault(chl=>chl.ID==t.ChlID).Name,
                    CtrlName = ctrlModels.FirstOrDefault(ctrl => (ctrl.ID == t.CtrlID && ctrl.ChlID == t.ChlID)).Name,
                    ValueType = (int)t.ValueType,
                    Timestamp = t.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    t.Status
                });

                var count = query.Count();
                return Json(new
                {
                    sEcho = sEcho,
                    iTotalRecords = count,
                    iTotalDisplayRecords = count,
                    data = query.Skip(start).Take(length)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { data = new List<object>() }, JsonRequestBehavior.AllowGet);
            }

            //return Json(new { data = ls }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetVariableValue()
        {
            try
            {
                var authkey = Request["authkey"];
                if (string.IsNullOrEmpty(authkey))
                    return Json(new { success = false, message = "authkey" });

                var data = Request["data"];
                var query = JsonConvert.DeserializeObject<List<DevVariable>>(data);
                List<DevVariable> result = new List<DevVariable>();
                foreach (var entity in query)
                {
                    var varvalue = DeviceManagement.SingletonInstance.VarGetValue(authkey, entity.ChlID, entity.CtrlID, entity.ID);
                    var varModel = JSONFormatter.Deserialize<DevVariable>(varvalue);
                    result.Add(varModel);
                }
                var qu = result.Select(t => new
                {
                    ID = t.ID,
                    Name = t.Name,
                    Value = t.Value,
                    CtrlID = t.CtrlID,
                    ChlID = t.ChlID,
                    ValueType = (int)t.ValueType,
                    Timestamp = t.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    Status = t.Status
                });

                return Json(new { success = true, data = qu });
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
