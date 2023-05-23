using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Threading;
/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using GHIBMS.Common;
using GHIBMS.Interface;
using System.Diagnostics;
在此之后:
using System.Diagnostics;
using System.Text;
using System.Threading;
*/


namespace GHIBMS.Server
{
    public class TaskController
    {
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

        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {
            TimeDb.InitVariableList();
            if (thread != null)
                return;
            thread = new Thread(new ThreadStart(PerformTimeTask));
            //线程名，调试用
            thread.Name = "PerformTimeTask_thread";
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

            if (thread != null && thread.IsAlive)
            {
                try
                {
                    thread.Interrupt();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            thread = null;

        }

        //内部线程
        private void PerformTimeTask()
        {
            try
            {
                foreach (TimeTaskInfo task in TimeDb.TimeTaskList)
                {
                    task.DateStamp = DateTime.Now;
                    task.DateStamp2 = DateTime.Now;
                }

                while (active)
                {
                    try
                    {

                        foreach (TimeTaskInfo task in TimeDb.TimeTaskList)
                        {
                            if (task.Enable && task.JudgeWeekEnable())
                            {
                                DateTime beginTime = Convert.ToDateTime(task.BeginTime);
                                DateTime endTime = Convert.ToDateTime(task.EndTime);
                                if (task.DateStamp2 < DateTime.Now)
                                {
                                    if (task.KeepAction)
                                    {
                                        task.DateStamp2 = DateTime.Now.AddSeconds(10); //每10秒检查一次
                                        if (DateTime.Now >= beginTime && endTime >= DateTime.Now)
                                        {
                                            //Debug.WriteLine("定时任务，DateStamp" + task.DateStamp.ToString() + "   DateTime.Now" + DateTime.Now.ToString());
                                            foreach (IVariable var in task.VarableList)
                                            {
                                                if (var.Value.ToString() != task.TaskValue.ToString())
                                                    var.WriteValue(task.TaskValue);
                                            }
                                        }
                                    }
                                }
                                if (task.DateStamp < DateTime.Now)
                                {
                                    if (!task.KeepAction)
                                    {
                                        if (DateTime.Now >= beginTime && task.TryCount < 3)
                                        {

                                            task.DateStamp = DateTime.Now.AddSeconds(10); //每10秒检查一次

                                            //Debug.WriteLine("定时任务，DateStamp" + task.DateStamp.ToString() + "   DateTime.Now" + DateTime.Now.ToString());
                                            foreach (IVariable var in task.VarableList)
                                            {
                                                var.WriteValue(task.TaskValue);
                                            }
                                            task.TryCount++;
                                            if (task.tryCount == 2)
                                            {
                                                task.DateStamp = beginTime.AddDays(1);
                                                task.tryCount = 0;
                                            }
                                        }
                                    }

                                }
                            }
                        }


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
                        Console.WriteLine(e.ToString());
                        System.Threading.Thread.Sleep(1000);
                        Logger.GetInstance().LogError(e.ToString());
                    }
                    Thread.Sleep(10);
                }
                thread = null;
            }
            catch {
                thread = null;
            }
        }
    }
}

