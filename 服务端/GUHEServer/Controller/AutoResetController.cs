
/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using System;
在此之后:
using GHIBMS.Interface;
using System;
*/
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GHIBMS.Server
{
    public class AutoResetController
    {

        public static List<IVariable> AutoResetVarList = new List<IVariable>();

        private int SLEEP_TIME = 1000;
        private bool active = false;
        private Thread thread;

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
        private object synObj = new object();

        public void AddVariable(IVariable var)
        {
            lock (synObj)
            {
                if (!AutoResetVarList.Contains(var))
                    AutoResetVarList.Add(var);
            }
        }
        //取得信息记录列表
        private void ResetVariable()
        {
            lock (synObj)//锁定信息记录列表
            {
                IVariable[] varArray = AutoResetVarList.ToArray();
                foreach (IVariable var in varArray)
                {
                    if (DateTime.Now >= var.DateStamp.AddSeconds(var.AutoResetDelay))
                    {
                        //var.UpdateValue(var.DefaultValue);
                        var.WriteValue(var.DefaultValue);
                        AutoResetVarList.Remove(var);
                    }
                }
            }
        }

        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {

            thread = new Thread(new ThreadStart(PerformResetTask));
            //线程名，调试用
            thread.Name = "PerformResetTask_thread";
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
            for (int i = 0; i < 20; i++)
            {
                if (thread != null)
                    Thread.Sleep(100);
                else
                    break;
            }
            if (thread != null)
            {
                try
                {
                    thread.Interrupt();
                }
                catch (Exception ex)
                {
                }
            }

            thread = null;

        }

        //内部线程
        private void PerformResetTask()
        {
            try
            {
                while (active)
                {
                    try
                    {
                        ResetVariable();

                        try
                        {
                            if (SLEEP_TIME < 100) SLEEP_TIME = 100;
                            for (int i = 0; i < (int)(SLEEP_TIME / 100); i++)
                            {
                                if (!active)
                                {
                                    thread = null;
                                    return;
                                }
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                        catch { }
                    }
                    catch (Exception e)
                    {
                        // Console.WriteLine(e.ToString());
                        System.Threading.Thread.Sleep(100);
                    }
                    Thread.Sleep(10);
                }
                thread = null;
            }catch
            {
                thread = null;
            }
        }
    }
}
